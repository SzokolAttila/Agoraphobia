using System.Xml;
using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;
using static AgoraphobiaLibrary.Item;

namespace AgoraphobiaAPI.Repositories;

public class ConsumableRepository : IConsumableRepository
{
    private readonly ApplicationDBContext _context;
    public ConsumableRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Consumable>> GetAllAsync()
    {
        return await _context.Consumables.ToListAsync();
    }

    public async Task<Consumable?> GetByIdAsync(int id)
    {
        return await _context.Consumables.FindAsync(id);
    }

    public async Task<Consumable> CreateAsync(Consumable consumableModel)
    {
        await _context.Consumables.AddAsync(consumableModel);
        await _context.SaveChangesAsync();
        return consumableModel;
    }

    public async Task<Consumable?> UpdateAsync(int id, CreateConsumableRequestDto consumableDto)
    {
        var consumableModel = await _context.Consumables.FirstOrDefaultAsync(x => x.Id == id);
        if (consumableModel is null)
            return null;
        
        consumableModel.Name = consumableDto.Name;
        consumableModel.Description = consumableDto.Description;
        consumableModel.Rarity = (ItemRarity)consumableDto.RarityIdx;
        consumableModel.Price = consumableDto.Price;
        consumableModel.Energy = consumableDto.Energy;
        consumableModel.Hp = consumableDto.Hp;
        consumableModel.Defense = consumableDto.Defense;
        consumableModel.Attack = consumableDto.Attack;
        consumableModel.Duration = consumableDto.Duration;
        await _context.SaveChangesAsync();
        return consumableModel;
    }

    public async Task<Consumable?> DeleteAsync(int id)
    {
        var consumableModel = await _context.Consumables.FirstOrDefaultAsync(x => x.Id == id);
        if (consumableModel is null)
            return null;
        _context.Consumables.Remove(consumableModel);
        await _context.SaveChangesAsync();
        return consumableModel;
    }
}