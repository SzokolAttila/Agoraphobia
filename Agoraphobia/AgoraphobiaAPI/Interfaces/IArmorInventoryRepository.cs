using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorInventoryRepository
{
    public Task<List<Armor>> GetArmorsAsync(int id);
}