using WasteGlassAPI.Models;

namespace WasteGlassAPI.Repositories;

public interface ICollectionRepository
{
    Task<List<Collection>> GetAllAsync();
    Task<Collection>       AddAsync(Collection collection);
}
