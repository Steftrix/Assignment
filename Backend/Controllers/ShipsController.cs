using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ShipsController : ControllerBase
{
    private readonly MaritimeDbContext _context;

    public ShipsController(MaritimeDbContext context)
    {
        _context = context;
    }

    // GET: api/ships
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ship>>> GetShips()
    {
        return await _context.Ships.ToListAsync();
    }

    // GET: api/ships/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ship>> GetShip(int id)
    {
        var ship = await _context.Ships.FindAsync(id);

        if (ship == null)
        {
            return NotFound();
        }

        return ship;
    }

    // POST: api/ships
    [HttpPost]
    public async Task<ActionResult<Ship>> CreateShip(Ship ship)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Ships.Add(ship);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShip), new { id = ship.ShipId }, ship);
    }

    // PUT: api/ships/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShip(int id, Ship ship)
    {
        if (id != ship.ShipId)
        {
            return BadRequest("ID mismatch");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(ship).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShipExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/ships/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShip(int id)
    {
        var ship = await _context.Ships.FindAsync(id);
        if (ship == null)
        {
            return NotFound();
        }

        _context.Ships.Remove(ship);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShipExists(int id)
    {
        return _context.Ships.Any(e => e.ShipId == id);
    }
}