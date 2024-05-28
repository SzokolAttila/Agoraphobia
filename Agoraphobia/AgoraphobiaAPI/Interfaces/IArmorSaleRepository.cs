using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IArmorSaleRepository
    {
        public Task<List<ArmorSale>> GetArmorSalesAsync(int merchantId);
    }
}
