using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableSale
{
    public class ConsumableSaleDto
    {
        public ConsumableDto Consumable { get; set; } = new();
        public int Quantity { get; set; }
    }
}
