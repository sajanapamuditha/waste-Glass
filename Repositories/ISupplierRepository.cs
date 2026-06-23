using WasteGlassAPI.Models;

namespace WasteGlassAPI.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();
    Task<Supplier?>      GetByIdAsync(int id);
    Task                 UpdateStatusAsync(int id, string status);
}
