using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class DataVisController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DataVisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        [HttpGet("linechart/years")]
        public async Task<IActionResult> GetAvailableYears()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                SELECT DISTINCT CAST(EXTRACT(YEAR FROM voyage_start) AS INT) AS year
                FROM voyages
                ORDER BY year DESC", conn);

            using var reader = await cmd.ExecuteReaderAsync();
            var years = new List<int>();

            while (await reader.ReadAsync())
            {
                years.Add(reader.GetInt32(0)); // Read as an integer
            }

            return Ok(years);
        }

        [HttpGet("linechart")]
        public async Task<IActionResult> GetTopDepartures([FromQuery] int year)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                SELECT 
                    c.country_name AS CountryName,
                    COUNT(v.voyage_id) AS DepartureCount
                FROM 
                    voyages v
                JOIN 
                    ports p ON v.departure_port = p.port_id
                JOIN 
                    countries c ON p.country_id = c.country_id
                WHERE 
                    EXTRACT(YEAR FROM v.voyage_start) = @year
                GROUP BY 
                    c.country_name
                ORDER BY 
                    COUNT(v.voyage_id) DESC
                LIMIT 10", conn);

            cmd.Parameters.AddWithValue("year", year);

            using var reader = await cmd.ExecuteReaderAsync();
            var results = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>
                {
                    ["CountryName"] = reader.GetString(0),
                    ["DepartureCount"] = reader.GetInt64(1)
                };
                results.Add(row);
            }


            return Ok(results);
        }
    }
}