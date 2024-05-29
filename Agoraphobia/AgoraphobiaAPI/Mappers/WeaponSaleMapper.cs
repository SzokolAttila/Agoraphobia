using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Mappers
{
    public static class WeaponSaleMapper
    {
        public static WeaponSaleDto ToWeaponSaleDto(this WeaponSale weaponSale)
        {
            return new WeaponSaleDto
            {
                Weapon = weaponSale.Weapon!.ToWeaponDto(),
                Quantity = weaponSale.Quantity
            };
        }
        public static UpdateWeaponSaleRequestDto ToUpdateWeaponSaleRequestDto(this WeaponSale weaponSale)
        {
            return new UpdateWeaponSaleRequestDto
            {
                MerchantId = weaponSale.MerchantId,
                Weapon = weaponSale.Weapon!.ToWeaponDto(),
                Quantity = weaponSale.Quantity
            };
        }
    }
}
