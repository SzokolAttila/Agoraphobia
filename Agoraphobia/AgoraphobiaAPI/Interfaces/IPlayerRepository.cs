using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IPlayerRepository
{
    Task<List<Player>> GetAllAsync();
    Task<Player?> GetByIdAsync(int id);
    Task<Player?> CreateAsync(Player player);
    Task<Player?> DeleteAsync(int id);
    Task<Player?> UpdateAsync(int id, UpdatePlayerRequestDto player);
}