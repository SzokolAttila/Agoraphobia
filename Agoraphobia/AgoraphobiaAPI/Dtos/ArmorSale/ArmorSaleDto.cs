using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorSale
{
    public class ArmorSaleDto
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int ArmorId { get; set; }
        public ArmorDto Armor { get; set; } = new();
        public int Quantity { get; set; }
    }
}
