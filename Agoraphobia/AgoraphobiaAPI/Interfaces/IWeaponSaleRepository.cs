using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IWeaponSaleRepository
    {
        public Task<List<WeaponSale>> GetWeaponSalesAsync(int merchantId);
        public Task<WeaponSale> CreateAsync(WeaponSale weaponSale);
        public Task<WeaponSale?> AddOneAsync(int id);
        public Task<WeaponSale?> GetByIdAsync(int id);
        public Task<WeaponSale?> DeleteAsync(int id);
        public Task<WeaponSale?> RemoveOneAsync(int id);
    }
}
