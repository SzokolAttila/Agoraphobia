using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorDroprateRepository
{
    public Task<List<ArmorDroprate>> GetArmorDropratesAsync(int id);
    public Task<ArmorDroprate> CreateAsync(ArmorDroprate armorDroprate);
    public Task<ArmorDroprate?> DeleteAsync(ArmorDroprate armorDroprate);
}