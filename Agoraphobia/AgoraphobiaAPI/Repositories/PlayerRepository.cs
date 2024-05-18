using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDBContext _context;
    public PlayerRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Player>> GetAllAsync()
    {
        return await _context.Players
            .Include(x => x.WeaponInventories)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.ArmorInventories)
            .ThenInclude(x => x.Armor)
            .ToListAsync();
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        return await _context.Players
            .Include(x => x.WeaponInventories)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.ArmorInventories)
            .ThenInclude(x => x.Armor)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Player?> CreateAsync(Player player)
    {
        if (!_context.Accounts.Any(x => x.Id == player.AccountId))
            return null;
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<Player?> DeleteAsync(int id)
    {
        var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        if (player is null)
            return null;
        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<Player?> UpdateAsync(int id, UpdatePlayerRequestDto playerDto)
    {
        var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        if (player is null)
            return null;

        player.Attack = playerDto.Attack;
        player.Defense = playerDto.Defense;
        player.Sanity = playerDto.Sanity;
        player.MaxEnergy = playerDto.MaxEnergy;
        player.Energy = playerDto.Energy;
        player.DreamCoins = playerDto.DreamCoins;
        player.MaxHealth = playerDto.MaxHealth;
        player.Health = playerDto.Health;

        await _context.SaveChangesAsync();
        return player;
    }
}