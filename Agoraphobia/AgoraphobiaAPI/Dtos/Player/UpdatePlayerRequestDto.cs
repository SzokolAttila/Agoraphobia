namespace AgoraphobiaAPI.Dtos.Player;

public class UpdatePlayerRequestDto
{
    public double Sanity { get; set; }
    public double MaxHealth { get; set; }
    public double Health { get; set; }
    public int MaxEnergy { get; set; }
    public int Energy { get; set; }
    public double Attack { get; set; }
    public double Defense { get; set; }
    public int DreamCoins { get; set; }
}
