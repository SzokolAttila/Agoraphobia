using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableDroprate
{
    public class ConsumableDroprateDto
    {
        public ConsumableDto Consumable { get; set; } = new();
        public double Droprate { get; set; }
    }
}
