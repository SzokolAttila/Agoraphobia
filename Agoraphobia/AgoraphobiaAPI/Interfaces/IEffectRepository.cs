using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IEffectRepository
{
    public Task<List<Effect>> GetEffectsAsync(int id);
    public Task<Effect> CreateAsync(Effect effect);
    public Task<Effect?> AddOneAsync(EffectRequestDto update);
    public Task<Effect?> DeleteAsync(Effect effect);
    public Task<Effect?> RemoveOneAsync(EffectRequestDto update);
}