using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class AccountMappers
{
    public static Account ToAccountFromCreateDto(this CreateAccountRequestDto account)
    {
        return new Account(account.Username, account.Passwd, account.IsPasswordHashed);
    }
}