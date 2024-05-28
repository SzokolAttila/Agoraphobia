using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponSale
{
    public class WeaponSaleDto
    {
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
