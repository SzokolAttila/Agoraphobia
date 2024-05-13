namespace AgoraphobiaAPI.Dtos.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Passwd { get; set; } = string.Empty;
    public bool IsPasswordHashed { get; set; }
}