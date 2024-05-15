using System.Xml;
using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;
using static AgoraphobiaLibrary.Item;

namespace AgoraphobiaAPI.Repositories;

public class ArmorRepository : IArmorRepository
{
    private readonly ApplicationDBContext _context;
    public ArmorRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Armor>> GetAllAsync()
    {
        return await _context.Armors.ToListAsync();
    }

    public async Task<Armor?> GetByIdAsync(int id)
    {
        return await _context.Armors.FindAsync(id);
    }

    public async Task<Armor> CreateAsync(Armor armorModel)
    {
        await _context.Armors.AddAsync(armorModel);
        await _context.SaveChangesAsync();
        return armorModel;
    }

    public async Task<Armor?> UpdateAsync(int id, CreateArmorRequestDto armorDto)
    {
        var armorModel = await _context.Armors.FirstOrDefaultAsync(x => x.Id == id);
        if (armorModel is null)
            return null;
        
        armorModel.Name = armorDto.Name;
        armorModel.Description = armorDto.Description;
        armorModel.Rarity = (ItemRarity)armorDto.RarityIdx;
        armorModel.Price = armorDto.Price;
        armorModel.Defense = armorDto.Defense;
        armorModel.Hp = armorDto.Hp;
        armorModel.ArmorTypeIdx = armorDto.ArmorTypeIdx;
        await _context.SaveChangesAsync();
        return armorModel;
    }

    public async Task<Armor?> DeleteAsync(int id)
    {
        var armorModel = await _context.Armors.FirstOrDefaultAsync(x => x.Id == id);
        if (armorModel is null)
            return null;
        _context.Armors.Remove(armorModel);
        await _context.SaveChangesAsync();
        return armorModel;
    }
}