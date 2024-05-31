using System.Text;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Account;
using AgoraphobiaLibrary.Exceptions.Password;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class AccountHttpClient
    {
        private readonly HttpClient _httpClient;
        public AccountHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string ROUTE = "http://localhost:5172/agoraphobia/";

        public async Task<Account> Register(string username, string password, bool isPasswordHashed)
        {
            var accounts = await _httpClient.GetAsync($"{ROUTE}accounts");
            if (accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!.Exists(x => x.Username == username))
                throw new NonUniqueUsernameException();
            var account = new Account(username, password, isPasswordHashed);
            var content = new StringContent(JsonConvert.SerializeObject(new CreateAccountRequestDto
            {
                IsPasswordHashed = isPasswordHashed,
                Username = username,
                Passwd = password
            }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ROUTE}accounts", content);
            response.EnsureSuccessStatusCode();
            return account;
        }

        public async Task<Account> LogIn(string username, string password, bool isPasswordHashed)
        {
            var accounts = await _httpClient.GetAsync($"{ROUTE}accounts");
            var account = accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!.FirstOrDefault(x =>
                x.Username == username && x.Password == new Password(password, isPasswordHashed).Passwd);
            if (account is null)
                throw new InvalidLoginException();
            return new Account(account.Id, username, password, isPasswordHashed);
        }

        public async Task<HttpResponseMessage> GetAccounts()
        {
            return await _httpClient.GetAsync($"{ROUTE}accounts");
        }
    }
}
