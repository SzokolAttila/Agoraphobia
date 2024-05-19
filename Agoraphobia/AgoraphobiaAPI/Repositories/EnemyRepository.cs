using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Enemy;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class EnemyRepository : IEnemyRepository
{
    private readonly ApplicationDBContext _context;
    public EnemyRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Enemy>> GetAllAsync()
    {
        return await _context.Enemies
            .Include(x => x.ArmorDroprates)
            .ThenInclude(x => x.Armor)
            .Include(x => x.ConsumableDroprates)
            .ThenInclude(x => x.Consumable)
            .ToListAsync();
    }

    public async Task<Enemy?> GetByIdAsync(int id)
    {
        return await _context.Enemies
            .Include(x => x.ArmorDroprates)
            .ThenInclude(x => x.Armor)
            .Include(x => x.WeaponDroprates)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.ConsumableDroprates)
            .ThenInclude(x => x.Consumable)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Enemy?> CreateAsync(Enemy enemy)
    {
        await _context.Enemies.AddAsync(enemy);
        await _context.SaveChangesAsync();
        return enemy;
    }

    public async Task<Enemy?> DeleteAsync(int id)
    {
        var enemy = await _context.Enemies.FirstOrDefaultAsync(x => x.Id == id);
        if (enemy is null)
            return null;
        _context.Enemies.Remove(enemy);
        await _context.SaveChangesAsync();
        return enemy;
    }

    public async Task<Enemy?> UpdateAsync(int id, UpdateEnemyRequestDto enemyDto)
    {
        var enemy = await _context.Enemies.FirstOrDefaultAsync(x => x.Id == id);
        if (enemy is null)
            return null;

        enemy.Attack = enemyDto.Attack;
        enemy.Defense = enemyDto.Defense;
        enemy.Sanity = enemyDto.Sanity;
        enemy.DreamCoins = enemyDto.DreamCoins;
        enemy.Hp = enemyDto.Hp;
        //enemy.Armors = enemyDto.Armors;
        //enemy.Weapons = enemyDto.Weapons;
        //enemy.Consumables = enemyDto.Consumables;

        await _context.SaveChangesAsync();
        return enemy;
    }
}