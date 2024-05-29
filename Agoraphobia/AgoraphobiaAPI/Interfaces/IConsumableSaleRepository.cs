using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IConsumableSaleRepository
    {
        public Task<List<ConsumableSale>> GetConsumableSalesAsync(int merchantId);
    }
}
