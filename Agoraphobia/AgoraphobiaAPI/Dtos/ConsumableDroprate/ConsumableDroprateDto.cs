using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableDroprate
{
    public class ConsumableDroprateDto
    {
        public int Id { get; set; }
        public int ConsumableId { get; set; }
        public int EnemyId { get; set; }
        public ConsumableDto Consumable { get; set; } = new();
        public double Droprate { get; set; }
    }
}
