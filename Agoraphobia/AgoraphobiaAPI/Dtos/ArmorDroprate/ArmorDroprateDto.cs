using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorDroprate
{
    public class ArmorDroprateDto
    {
        public ArmorDto Armor { get; set; } = new();
        public double Droprate { get; set; }
    }
}
