namespace AgoraphobiaAPI.Dtos.WeaponDroprate
{
    public class WeaponDroprateRequestDto
    {
        public int EnemyId { get; set; }
        public int WeaponId { get; set; }
        public double Droprate { get; set; }
    }
}
