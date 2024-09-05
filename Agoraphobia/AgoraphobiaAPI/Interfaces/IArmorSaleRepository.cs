using AgoraphobiaAPI.Dtos.ArmorInventory;
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
        public Task<ArmorSale?> AddOneAsync(int id);
        public Task<ArmorSale?> DeleteAsync(int id);
        public Task<ArmorSale?> GetByIdAsync(int id);
        public Task<ArmorSale?> RemoveOneAsync(int id);
    }
}
