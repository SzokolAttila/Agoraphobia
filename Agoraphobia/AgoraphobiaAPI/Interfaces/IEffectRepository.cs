using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IEffectRepository
{
    public Task<List<Effect>> GetEffectsAsync(int playerId);
    public Task<Effect> CreateAsync(Effect effect);
    public Task<Effect?> GetByIdAsync(int id);
    public Task<Effect?> DeleteAsync(int id);
    public Task<Effect?> RemoveOneAsync(int id);
}