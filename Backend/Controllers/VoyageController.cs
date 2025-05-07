using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Models;
using Backend.Data;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoyageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VoyageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            return await FetchTable("countries");
        }

        [HttpGet("Ports")]
        public async Task<IActionResult> GetPorts()
        {
            return await FetchTable("ports");
        }

        [HttpGet("Ships")]
        public async Task<IActionResult> GetShips()
        {
            return await FetchTable("ships");
        }

        [HttpGet("voyages")]
        public async Task<IActionResult> GetVoyages()
        {
            return await FetchTable("voyages");
        }

        [HttpGet("LastYearCountries")]
        public async Task<IActionResult> GetLastYearCountries()
        {
            return await FetchTable("lastYearCountries");
        }
        private async Task<IActionResult> FetchTable(string tableName)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand($"SELECT * FROM {tableName}", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            var tableData = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.GetValue(i);
                tableData.Add(row);
            }

            return Ok(tableData);
        }

        // GET: api/voyage/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoyage(int id)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("SELECT * FROM voyages WHERE voyage_id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var voyage = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    voyage[reader.GetName(i)] = reader.GetValue(i);
                return Ok(voyage);
            }

            return NotFound();
        }

        // POST: api/voyage
        [HttpPost]
        public async Task<IActionResult> CreateVoyage([FromBody] Voyage voyage)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                INSERT INTO voyages (ship_id, departure_port, arrival_port, voyage_start, voyage_end)
                VALUES (@ship_id, @departure_port, @arrival_port, @voyage_start, @voyage_end)
                RETURNING voyage_id", conn);

            cmd.Parameters.AddWithValue("ship_id", voyage.ShipId);
            cmd.Parameters.AddWithValue("departure_port", voyage.DeparturePort);
            cmd.Parameters.AddWithValue("arrival_port", voyage.ArrivalPort);
            cmd.Parameters.AddWithValue("voyage_start", voyage.VoyageStart);
            cmd.Parameters.AddWithValue("voyage_end", voyage.VoyageEnd);

            try
            {
                int id = (int)await cmd.ExecuteScalarAsync();
                return CreatedAtAction(nameof(GetVoyage), new { id = id }, voyage);
            }
            catch (PostgresException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/voyage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoyage(int id, [FromBody] Voyage voyage)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                UPDATE voyages SET 
                    ship_id = @ship_id, 
                    departure_port = @departure_port, 
                    arrival_port = @arrival_port, 
                    voyage_start = @voyage_start, 
                    voyage_end = @voyage_end
                WHERE voyage_id = @id", conn);

            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("ship_id", voyage.ShipId);
            cmd.Parameters.AddWithValue("departure_port", voyage.DeparturePort);
            cmd.Parameters.AddWithValue("arrival_port", voyage.ArrivalPort);
            cmd.Parameters.AddWithValue("voyage_start", voyage.VoyageStart);
            cmd.Parameters.AddWithValue("voyage_end", voyage.VoyageEnd);

            int rows = await cmd.ExecuteNonQueryAsync();
            if (rows == 0)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/voyage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("DELETE FROM voyages WHERE voyage_id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            int rows = await cmd.ExecuteNonQueryAsync();
            if (rows == 0)
                return NotFound();

            return NoContent();
        }
    }
}
