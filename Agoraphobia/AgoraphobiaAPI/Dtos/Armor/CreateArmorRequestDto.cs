namespace AgoraphobiaAPI.Dtos.Armor
{
    public class CreateArmorRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RarityIdx { get; set; }
        public int Price { get; set; }
        public double Defense { get; set; }
        public double Hp { get; set; }
        public int ArmorTypeIdx { get; set; }
    }
}
