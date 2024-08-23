using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftLogsController(ShiftLoggerDbContext context) : ControllerBase
{
    private readonly ShiftLoggerDbContext _context = context;

    // GET: api/ShiftLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftLogDTO>>> GetShiftLogs()
    {
        return await _context.ShiftLogs.Select(sl => new ShiftLogDTO(sl)).ToListAsync();
    }

    // GET: api/ShiftLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftLogDTO>> GetShiftLog(int id)
    {
        var shiftLog = await _context.ShiftLogs.FindAsync(id);

        if (shiftLog == null)
        {
            return NotFound();
        }

        return new ShiftLogDTO(shiftLog);
    }

    // PUT: api/ShiftLogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShiftLog(int id, ShiftLogDTO shiftLogDto)
    {
        if (id != shiftLogDto.Id)
        {
            return BadRequest();
        }

        var shiftLog = new ShiftLog
        {
            Id = shiftLogDto.Id,
            FirstName = shiftLogDto.FirstName,
            LastName = shiftLogDto.LastName,
            ShiftStartTime = shiftLogDto.ShiftStartTime,
            ShiftEndTime = shiftLogDto.ShiftEndTime
        };
        _context.Entry(shiftLog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftLogExists(id))
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

    // POST: api/ShiftLogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ShiftLog>> PostShiftLog(ShiftLogDTO shiftLogDto)
    {
        var shiftLog = new ShiftLog
        {
            Id = shiftLogDto.Id,
            FirstName = shiftLogDto.FirstName,
            LastName = shiftLogDto.LastName,
            ShiftStartTime = shiftLogDto.ShiftStartTime,
            ShiftEndTime = shiftLogDto.ShiftEndTime
        };
        _context.ShiftLogs.Add(shiftLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShiftLog", new { id = shiftLog.Id }, shiftLog);
    }

    // DELETE: api/ShiftLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShiftLog(int id)
    {
        var shiftLog = await _context.ShiftLogs.FindAsync(id);
        if (shiftLog == null)
        {
            return NotFound();
        }

        _context.ShiftLogs.Remove(shiftLog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftLogExists(int id)
    {
        return _context.ShiftLogs.Any(e => e.Id == id);
    }
}
