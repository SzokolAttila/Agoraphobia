using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IWeaponSaleRepository
    {
        public Task<List<WeaponSale>> GetWeaponSalesAsync(int merchantId);
        public Task<WeaponSale> CreateAsync(WeaponSale weaponSale);
        public Task<WeaponSale?> AddOneAsync(WeaponSaleRequestDto update);
        public Task<WeaponSale?> DeleteAsync(WeaponSale weaponSale);
        public Task<WeaponSale?> RemoveOneAsync(WeaponSaleRequestDto update);
    }
}
