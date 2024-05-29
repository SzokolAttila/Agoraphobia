using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponSale
{
    public class UpdateWeaponSaleRequestDto
    {
        public int MerchantId { get; set; }
        public WeaponDto Weapon{ get; set; } = new();
        public int Quantity { get; set; }
    }
}
