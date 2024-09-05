using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableSale
{
    public class ConsumableSaleDto
    {
        public int Id { get; set; }
        public int ConsumableId { get; set; }
        public int MerchantId { get; set; }
        public ConsumableDto Consumable { get; set; } = new();
        public int Quantity { get; set; }
    }
}
