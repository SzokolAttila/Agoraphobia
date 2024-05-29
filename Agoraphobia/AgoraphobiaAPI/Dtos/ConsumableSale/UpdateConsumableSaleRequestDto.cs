using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableSale
{
    public class UpdateConsumableSaleRequestDto
    {
        public int MerchantId { get; set; }
        public ConsumableDto Consumable { get; set; } = new();
        public int Quantity { get; set; }
    }
}
