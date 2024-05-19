using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponDroprate
{
    public class WeaponDroprateDto
    {
        public WeaponDto Weapon { get; set; } = new();
        public double Droprate { get; set; }
    }
}
