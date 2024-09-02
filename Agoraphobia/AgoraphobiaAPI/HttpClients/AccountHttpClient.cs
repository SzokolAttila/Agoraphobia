using System.Text;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Account;
using AgoraphobiaLibrary.Exceptions.Password;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class AccountHttpClient : HttpClientBase
    {
        public static async Task<Account> Register(string username, string password, bool isPasswordHashed)
        {
            var accounts = await HttpClient.GetAsync($"{ROUTE}accounts");
            if (accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!
                .Exists(x => x.Username == username))
                throw new NonUniqueUsernameException();
            var account = new Account(username, password, isPasswordHashed);
            var content = new StringContent(JsonConvert.SerializeObject(new CreateAccountRequestDto
            {
                IsPasswordHashed = isPasswordHashed,
                Username = username,
                Passwd = password
            }), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}accounts", content);
            response.EnsureSuccessStatusCode();
            return account;
        }

        public static async Task<Account> LogIn(string username, string password, bool isPasswordHashed)
        {
            var accounts = await HttpClient.GetAsync($"{ROUTE}accounts");
            var account = accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!
                .Find(x => 
                    x.Username == username && x.Password == new Password(password, isPasswordHashed).Passwd);
            if (account is null)
                throw new InvalidLoginException();
            return new Account(account.Id, username, password, isPasswordHashed);
        }

        public static async Task<HttpResponseMessage> GetAccounts()
        {
            return await HttpClient.GetAsync($"{ROUTE}accounts");
        }
    }
}
