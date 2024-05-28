using AgoraphobiaAPI.Dtos.Merchant;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers
{
    public static class MerchantMapper
    {
        public static MerchantDto ToMerchantDto(this Merchant merchant)
        {
            return new MerchantDto
            {
                Id = merchant.Id,
                Name = merchant.Name,
                Description = merchant.Description,
                Armors = merchant.ArmorSales.Select(x => x.ToArmorSaleDto()).ToList(),
                Consumables = merchant.ConsumableSales.Select(x => x.ToConsumableSaleDto()).ToList(),
                Weapons = merchant.WeaponSales.Select(x => x.ToWeaponSaleDto()).ToList()
            };
        }
    }
}
