using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WasteGlassAPI.Data;

namespace WasteGlassAPI.Controllers
{
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
            await _context.Collections.ExecuteDeleteAsync();

            var suppliers = await _context.Suppliers.ToListAsync();

            foreach (var supplier in suppliers)
            {
                supplier.Status = "Pending";
            }

            var firstSupplier = suppliers.FirstOrDefault(x => x.Id == 2);

            if (firstSupplier != null)
            {
                firstSupplier.Status = "Next";
            }

            await _context.SaveChangesAsync();

            return Ok("Trip reset successfully");
        }
    }
}