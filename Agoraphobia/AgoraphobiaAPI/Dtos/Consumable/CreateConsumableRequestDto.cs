namespace AgoraphobiaAPI.Dtos.Consumable
{
    public class CreateConsumableRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RarityIdx { get; set; }
        public int Price { get; set; }
        public int Energy { get; set; }
        public int Hp { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int Duration { get; set; }
    }
}
