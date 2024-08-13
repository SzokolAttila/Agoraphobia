namespace AgoraphobiaAPI.Dtos.Consumable;

public class ConsumableDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RarityIdx { get; set; }
    public int Price { get; set; }
    public double Sanity { get; set; }
    public int Energy { get; set; }
    public double Hp { get; set; }
    public double Defense { get; set; }
    public double Attack { get; set; }
    public int Duration { get; set; }
}