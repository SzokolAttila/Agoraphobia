using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorLootRepository
{
    public Task<List<ArmorLoot>> GetArmorLootsAsync(int roomId);
    public Task<ArmorLoot> CreateAsync(ArmorLoot armorLoot);
    public Task<ArmorLoot?> GetByIdAsync(int id);
    public Task<ArmorLoot?> AddOneAsync(int id);
    public Task<ArmorLoot?> DeleteAsync(int id);
    public Task<ArmorLoot?> RemoveOneAsync(int id);
}