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
                MerchantId = armor.MerchantId,
                ArmorId = armor.ArmorId,
                Armor = armor.Armor!.ToArmorDto(),
                Quantity = armor.Quantity,
                Id = armor.Id,
            };
        }
    }
}
