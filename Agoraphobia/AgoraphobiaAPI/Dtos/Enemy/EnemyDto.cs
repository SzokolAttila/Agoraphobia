using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaAPI.Dtos.ArmorInventory;

namespace AgoraphobiaAPI.Dtos.Enemy
{
    public class EnemyDto
    {
        public int Id { get; set; }
        public double Sanity { get; set; }
        public double Hp { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public int DreamCoins { get; set; }
        public Dictionary<AgoraphobiaLibrary.Weapon, double> Weapons { get; set; } = new();
        public Dictionary<AgoraphobiaLibrary.Consumable, double> Consumables { get; set; } = new();
        public List<ArmorDroprateDto> Armors { get; set; }
    }
}
