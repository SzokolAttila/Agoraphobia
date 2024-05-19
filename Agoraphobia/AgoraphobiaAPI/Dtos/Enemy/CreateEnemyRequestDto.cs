using AgoraphobiaAPI.Dtos.ArmorDroprate;

namespace AgoraphobiaAPI.Dtos.Enemy
{
    public class CreateEnemyRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hp { get; set; }
        public double Defense { get; set; }
        public double Attack { get; set; }
        public double Sanity { get; set; }
        public int DreamCoins { get; set; }
    }
}
