using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ArmorInventoryRepository : IArmorInventoryRepository
{
    private readonly ApplicationDBContext _context;
    public ArmorInventoryRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Armor>> GetArmorsAsync(int playerId)
    {
        return await _context.ArmorInventories.Where(x => x.PlayerId == playerId)
            .Select(x => new Armor
                {
                    Id = x.ArmorId,
                    ArmorType = x.Armor!.ArmorType,
                    ArmorTypeIdx = x.Armor.ArmorTypeIdx,
                    Defense = x.Armor.Defense,
                    Description = x.Armor.Description,
                    Name = x.Armor.Name,
                    Price = x.Armor.Price,
                    Hp = x.Armor.Hp,
                    Rarity = x.Armor.Rarity,
                    RarityIdx = x.Armor.RarityIdx
                }
            ).ToListAsync();
    }
}