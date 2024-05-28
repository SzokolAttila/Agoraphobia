using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorSale
{
    public class UpdateArmorSaleRequestDto
    {
        public int MerchantId { get; set; }
        public ArmorDto Armor { get; set; } = new();
        public int Quantity { get; set; }
    }
}
