using Microsoft.AspNetCore.Mvc;
using WasteGlassAPI.Repositories;

namespace WasteGlassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierRepository _suppliers;

    public SuppliersController(ISupplierRepository suppliers)
    {
        _suppliers = suppliers;
    }

    /// GET /api/suppliers  – list all suppliers (for debugging)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _suppliers.GetAllAsync();
        return Ok(list);
    }

    /// GET /api/suppliers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var s = await _suppliers.GetByIdAsync(id);
        return s is null ? NotFound() : Ok(s);
    }

    /// PATCH /api/suppliers/{id}/reset  – reset status to Pending (for testing)
    [HttpPatch("{id}/reset")]
    public async Task<IActionResult> Reset(int id)
    {
        await _suppliers.UpdateStatusAsync(id, "Pending");
        return Ok(new { message = "Reset to Pending." });
    }
}
