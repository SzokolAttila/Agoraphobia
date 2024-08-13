using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class EffectRepository : IEffectRepository
{
    private readonly ApplicationDBContext _context;

    public EffectRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Effect>> GetEffectsAsync(int playerId)
    {
        return await _context.Effects.Where(x => x.PlayerId == playerId).ToListAsync();
    }

    public async Task<Effect> CreateAsync(Effect effect)
    {
        await _context.Effects.AddAsync(effect);
        await _context.SaveChangesAsync();
        return effect;
    }

    public async Task<Effect?> GetByIdAsync(int id)
    {
        return await _context.Effects
            .Include(x => x.Consumable)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Effect?> DeleteAsync(Effect effect)
    {
        var effectModel = _context.Effects.FirstOrDefault(
            x => x.PlayerId == effect.PlayerId && x.ConsumableId == effect.ConsumableId);
        if (effectModel is null)
            return null;
        _context.Effects.Remove(effectModel);
        await _context.SaveChangesAsync();
        return effectModel;
    }

    public async Task<Effect?> RemoveOneAsync(int id)
    {
        var effect = await _context.Effects.FirstOrDefaultAsync(x => x.Id == id);
        if (effect is null)
            return null;

        effect.CurrentDuration--;
        await _context.SaveChangesAsync();
        return effect;
    }
}