using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Mappers
{
    public static class ConsumableSaleMapper
    {
        public static ConsumableSaleDto ToConsumableSaleDto(this ConsumableSale consumableSale)
        {
            return new ConsumableSaleDto
            {
                Consumable = consumableSale.Consumable!.ToConsumableDto(),
                Quantity = consumableSale.Quantity
            };
        }
        public static UpdateConsumableSaleRequestDto ToUpdateConsumableSaleRequestDto(this ConsumableSale consumableSale)
        {
            return new UpdateConsumableSaleRequestDto
            {
                MerchantId = consumableSale.MerchantId,
                Consumable = consumableSale.Consumable!.ToConsumableDto(),
                Quantity = consumableSale.Quantity
            };
        }
    }
}
