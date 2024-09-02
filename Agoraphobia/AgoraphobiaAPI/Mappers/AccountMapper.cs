using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class AccountMapper
{
    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            Username = account.Username,
            Password = account.Passwd,
            IsPasswordHashed = account.IsPasswordHashed,
            Players = account.Players.Select(x => x.ToPlayerDto()).ToList()
        };
    }
}