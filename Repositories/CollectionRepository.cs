using Microsoft.EntityFrameworkCore;
using WasteGlassAPI.Data;
using WasteGlassAPI.Models;

namespace WasteGlassAPI.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _db;
    public CollectionRepository(AppDbContext db) => _db = db;

    public Task<List<Collection>> GetAllAsync() =>
        _db.Collections.Include(c => c.Supplier).ToListAsync();

    public async Task<Collection> AddAsync(Collection collection)
    {
        _db.Collections.Add(collection);
        await _db.SaveChangesAsync();
        return collection;
    }
}
