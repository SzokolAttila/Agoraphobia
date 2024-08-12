using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponSale
{
    public class WeaponSaleDto
    {
        public int WeaponId { get; set; }
        public int MerchantId { get; set; }
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
