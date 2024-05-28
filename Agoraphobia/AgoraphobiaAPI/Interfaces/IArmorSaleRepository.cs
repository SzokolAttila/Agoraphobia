using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IArmorSaleRepository
    {
        public Task<List<ArmorSale>> GetArmorSalesAsync(int merchantId);
        public Task<ArmorSale> CreateAsync(ArmorSale armorSale);
        public Task<ArmorSale?> AddOneAsync(ArmorSaleRequestDto update);
    }
}
