namespace AgoraphobiaAPI.Dtos.Enemy
{
    public class UpdateEnemyRequestDto
    {
        public double Hp { get; set; }
        public double Defense { get; set; }
        public double Attack { get; set; }
        public double Sanity { get; set; }
        public int DreamCoins { get; set; }
    }
}
