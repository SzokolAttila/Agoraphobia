using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Mappers
{
    public static class ArmorSaleMapper
    {
        public static ArmorSaleDto ToArmorSaleDto(this ArmorSale armor)
        {
            return new ArmorSaleDto
            {
                Armor = armor.Armor!.ToArmorDto(),
                Quantity = armor.Quantity
            };
        }

        public static UpdateArmorSaleRequestDto ToUpdateArmorSaleRequestDto(this ArmorSale armorSale)
        {
            return new UpdateArmorSaleRequestDto
            {
                MerchantId = armorSale.MerchantId,
                Armor = armorSale.Armor!.ToArmorDto(),
                Quantity = armorSale.Quantity
            };
        }
    }
}
