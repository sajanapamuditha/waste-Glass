using Microsoft.AspNetCore.Mvc;
using WasteGlassAPI.Data;
using WasteGlassAPI.DTOs;
using WasteGlassAPI.Models;
using WasteGlassAPI.Repositories;

namespace WasteGlassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionRepository _collections;
    private readonly ISupplierRepository   _suppliers;

    public CollectionsController(
        ICollectionRepository collections,
        ISupplierRepository   suppliers)
    {
        _collections = collections;
        _suppliers   = suppliers;
    }

    /// POST /api/collections  – single collection submission from Screen 2
    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] CollectionRequestDto dto)
    {
        var supplier = await _suppliers.GetByIdAsync(dto.SupplierId);
        if (supplier is null)
            return NotFound($"Supplier {dto.SupplierId} not found.");

        var record = new Collection
        {
            SupplierId = dto.SupplierId,
            ClearKg    = dto.ClearKg,
            ColouredKg = dto.ColouredKg,
            Condition  = dto.Condition,
            Timestamp  = dto.Timestamp == default ? DateTime.UtcNow : dto.Timestamp,
        };

        await _collections.AddAsync(record);
        await _suppliers.UpdateStatusAsync(dto.SupplierId, "Collected");

        return Ok(new { message = "Collection recorded.", id = record.Id });
    }

    /// POST /api/collections/sync  – batch sync from Screen 3
    [HttpPost("sync")]
    public async Task<IActionResult> Sync([FromBody] List<CollectionRequestDto> dtos)
    {
        foreach (var dto in dtos)
        {
            var supplier = await _suppliers.GetByIdAsync(dto.SupplierId);
            if (supplier is null) continue;

            var record = new Collection
            {
                SupplierId = dto.SupplierId,
                ClearKg    = dto.ClearKg,
                ColouredKg = dto.ColouredKg,
                Condition  = dto.Condition,
                Timestamp  = dto.Timestamp == default ? DateTime.UtcNow : dto.Timestamp,
            };

            await _collections.AddAsync(record);
            await _suppliers.UpdateStatusAsync(dto.SupplierId, "Collected");
        }

        return Ok(new { message = $"Synced {dtos.Count} record(s)." });
    }
}
