using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorSale
{
    public class ArmorSaleDto
    {
        public ArmorDto Armor { get; set; } = new();
        public int Quantity { get; set; }
    }
}
