using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IWeaponSaleRepository
    {
        public Task<List<WeaponSale>> GetWeaponSalesAsync(int merchantId);
        public Task<WeaponSale> CreateAsync(WeaponSale weaponSale);
        public Task<WeaponSale?> AddOneAsync(WeaponSaleRequestDto update);
    }
}
