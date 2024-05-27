using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorLootRepository
{
    public Task<List<ArmorLoot>> GetArmorLootsAsync(int id);
    public Task<ArmorLoot> CreateAsync(ArmorLoot armorLoot);
    public Task<ArmorLoot?> AddOneAsync(ArmorLootRequestDto update);
    public Task<ArmorLoot?> DeleteAsync(ArmorLoot armorLoot);
    public Task<ArmorLoot?> RemoveOneAsync(ArmorLootRequestDto update);
}