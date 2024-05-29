using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IConsumableSaleRepository
    {
        public Task<List<ConsumableSale>> GetConsumableSalesAsync(int merchantId); 
        public Task<ConsumableSale> CreateAsync(ConsumableSale consumableSale);
        public Task<ConsumableSale?> AddOneAsync(ConsumableSaleRequestDto update);
    }
}
