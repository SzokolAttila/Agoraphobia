using AgoraphobiaAPI.Dtos.Player;

namespace AgoraphobiaAPI.Dtos.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsPasswordHashed { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
}