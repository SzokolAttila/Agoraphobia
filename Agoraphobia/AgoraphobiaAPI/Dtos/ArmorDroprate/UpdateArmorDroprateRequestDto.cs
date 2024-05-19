using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorDroprate
{
    public class UpdateArmorDroprateRequestDto
    {
        public int EnemyId { get; set; }
        public ArmorDto Armor { get; set; } = new();
        public double Droprate { get; set; }
    }
}
