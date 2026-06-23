using Microsoft.EntityFrameworkCore;
using WasteGlassAPI.Data;
using WasteGlassAPI.Models;

namespace WasteGlassAPI.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _db;
    public SupplierRepository(AppDbContext db) => _db = db;

    public Task<List<Supplier>> GetAllAsync() =>
        _db.Suppliers.ToListAsync();

    public Task<Supplier?> GetByIdAsync(int id) =>
        _db.Suppliers.FirstOrDefaultAsync(s => s.Id == id);

    public async Task UpdateStatusAsync(int id, string status)
    {
        var supplier = await _db.Suppliers.FindAsync(id);
        if (supplier is null) return;
        supplier.Status = status;
        await _db.SaveChangesAsync();
    }
}
