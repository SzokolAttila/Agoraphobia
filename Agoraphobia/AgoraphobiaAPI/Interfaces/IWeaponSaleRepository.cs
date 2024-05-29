using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IWeaponSaleRepository
    {
        public Task<List<WeaponSale>> GetWeaponSalesAsync(int merchantId);
    }
}
