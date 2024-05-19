using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableDroprate
{
    public class UpdateConsumableDroprateRequestDto
    {
        public int EnemyId { get; set; }
        public ConsumableDto Consumable { get; set; } = new();
        public double Droprate { get; set; }
    }
}
