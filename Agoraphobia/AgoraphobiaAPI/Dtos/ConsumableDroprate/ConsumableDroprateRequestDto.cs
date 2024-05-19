namespace AgoraphobiaAPI.Dtos.ConsumableDroprate
{
    public class ConsumableDroprateRequestDto
    {
        public int EnemyId { get; set; }
        public int ConsumableId { get; set; }
        public double Droprate { get; set; }
    }
}
