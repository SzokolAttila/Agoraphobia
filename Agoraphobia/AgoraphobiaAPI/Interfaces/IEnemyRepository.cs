using AgoraphobiaAPI.Dtos.Enemy;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IEnemyRepository
{
    Task<List<Enemy>> GetAllAsync();
    Task<Enemy?> GetByIdAsync(int id);
    Task<Enemy?> CreateAsync(Enemy enemy);
    Task<Enemy?> DeleteAsync(int id);
    Task<Enemy?> UpdateAsync(int id, UpdateEnemyRequestDto enemy);
}