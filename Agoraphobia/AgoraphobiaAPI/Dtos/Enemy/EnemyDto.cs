using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaAPI.Dtos.ConsumableDroprate;

namespace AgoraphobiaAPI.Dtos.Enemy
{
    public class EnemyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Sanity { get; set; }
        public double Hp { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public int DreamCoins { get; set; }
        public List<WeaponDroprateDto> WeaponDroprates { get; set; } = new();
        public List<ConsumableDroprateDto> ConsumableDroprates { get; set; } = new();
        public List<ArmorDroprateDto> ArmorDroprates { get; set; }
    }
}
