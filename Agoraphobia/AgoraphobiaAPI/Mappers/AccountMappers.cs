using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class AccountMappers
{
    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            Username = account.Username,
            Passwd = account.Passwd,
            IsPasswordHashed = true
        };
    }
    public static Account ToAccountFromCreateDto(this CreateAccountRequestDto account)
    {
        return new Account(account.Username, account.Passwd, account.IsPasswordHashed);
    }
}