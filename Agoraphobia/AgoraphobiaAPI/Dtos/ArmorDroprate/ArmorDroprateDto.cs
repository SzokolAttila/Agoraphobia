using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorDroprate
{
    public class ArmorDroprateDto
    {
        public int Id { get; set; }
        public int ArmorId { get; set; }
        public int EnemyId { get; set; }
        public ArmorDto Armor { get; set; } = new();
        public double Droprate { get; set; }
    }
}
