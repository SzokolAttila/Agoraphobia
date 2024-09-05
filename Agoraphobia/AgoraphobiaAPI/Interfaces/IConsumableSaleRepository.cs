using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IConsumableSaleRepository
    {
        public Task<List<ConsumableSale>> GetConsumableSalesAsync(int merchantId); 
        public Task<ConsumableSale> CreateAsync(ConsumableSale consumableSale);
        public Task<ConsumableSale?> AddOneAsync(int id);
        public Task<ConsumableSale?> GetByIdAsync(int id);
        public Task<ConsumableSale?> DeleteAsync(int id);
        public Task<ConsumableSale?> RemoveOneAsync(int id);
    }
}
