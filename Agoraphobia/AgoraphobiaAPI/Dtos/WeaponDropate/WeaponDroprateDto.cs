using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponDroprate
{
    public class WeaponDroprateDto
    {
        public int Id { get; set; }
        public int WeaponId { get; set; }
        public int EnemyId { get; set; }
        public WeaponDto Weapon { get; set; } = new();
        public double Droprate { get; set; }
    }
}
