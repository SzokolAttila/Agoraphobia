using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponDroprate
{
    public class UpdateWeaponDroprateRequestDto
    {
        public int EnemyId { get; set; }
        public WeaponDto Weapon { get; set; } = new();
        public double Droprate { get; set; }
    }
}
