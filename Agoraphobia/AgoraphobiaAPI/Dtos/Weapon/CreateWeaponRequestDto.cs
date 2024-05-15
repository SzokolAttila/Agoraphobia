namespace AgoraphobiaAPI.Dtos.Weapon
{
    public class CreateWeaponRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RarityIdx { get; set; }
        public int Price { get; set; }
        public double MinMultiplier { get; set; }
        public double MaxMultiplier { get; set; }
        public int Energy { get; set; }


    }
}
