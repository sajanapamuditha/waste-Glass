using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WasteGlassAPI.Data;

namespace WasteGlassAPI.Controllers;

[ApiController]
[Route("api")]
public class ResetController : ControllerBase
{
    private readonly AppDbContext _context;

    public ResetController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Reset()
    {
        // Delete all collections
        await _context.Collections.ExecuteDeleteAsync();

        // Reset supplier statuses
        var suppliers = await _context.Suppliers.ToListAsync();

        foreach (var supplier in suppliers)
        {
            supplier.Status = "Pending";
        }

        // First stop becomes Next
        var firstStop = suppliers.FirstOrDefault(s => s.Id == 2);

        if (firstStop != null)
        {
            firstStop.Status = "Next";
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Trip reset successfully"
        });
    }
}