using AgoraphobiaAPI.Dtos.WeaponSale;
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
    }
}
