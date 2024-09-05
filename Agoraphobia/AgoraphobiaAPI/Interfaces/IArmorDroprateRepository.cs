using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorDroprateRepository
{
    public Task<List<ArmorDroprate>> GetArmorDropratesAsync(int enemyId);
    public Task<ArmorDroprate> CreateAsync(ArmorDroprate armorDroprate);
    public Task<ArmorDroprate?> GetByIdAsync(int id);
    public Task<ArmorDroprate?> DeleteAsync(int id);
}