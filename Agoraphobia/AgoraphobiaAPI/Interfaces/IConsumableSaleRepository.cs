using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IConsumableSaleRepository
    {
        public Task<List<ConsumableSale>> GetConsumableSalesAsync(int merchantId); 
        public Task<ConsumableSale> CreateAsync(ConsumableSale consumableSale);
        public Task<ConsumableSale?> AddOneAsync(ConsumableSaleRequestDto update);
        public Task<ConsumableSale?> DeleteAsync(ConsumableSale consumableSale);
        public Task<ConsumableSale?> RemoveOneAsync(ConsumableSaleRequestDto update);
    }
}
