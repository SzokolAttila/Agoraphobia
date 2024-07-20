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
                ArmorSales = merchant.ArmorSales.Select(x => x.ToArmorSaleDto()).ToList(),
                ConsumableSales = merchant.ConsumableSales.Select(x => x.ToConsumableSaleDto()).ToList(),
                WeaponSales = merchant.WeaponSales.Select(x => x.ToWeaponSaleDto()).ToList()
            };
        }

        public static Merchant ToMerchantFromCreateDto(this MerchantRequestDto merchant)
        {
            return new Merchant(merchant.Name, merchant.Description);
        }
    }
}
