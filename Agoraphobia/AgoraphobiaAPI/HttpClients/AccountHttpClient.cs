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

        public async Task<Account> Register(string username, string password, bool isPasswordHashed)
        {
            var accounts = await _httpClient.GetAsync("http://localhost:5172/agoraphobia/accounts");
            if (accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!.Exists(x => x.Username == username))
                throw new NonUniqueUsernameException();
            var account = new Account(username, password, isPasswordHashed);
            var content = new StringContent(JsonConvert.SerializeObject(new CreateAccountRequestDto
            {
                IsPasswordHashed = false,
                Username = username,
                Passwd = password
            }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5172/agoraphobia/accounts", content);
            response.EnsureSuccessStatusCode();
            return account;
        }

        public async Task<Account> LogIn(string username, string password, bool isPasswordHashed)
        {
            var accounts = await _httpClient.GetAsync("http://localhost:5172/agoraphobia/accounts");
            var account = accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!.FirstOrDefault(x =>
                x.Username == username && x.Password == new Password(password, isPasswordHashed).Passwd);
            if (account is null)
                throw new InvalidLoginException();
            return new Account(username, password, isPasswordHashed);
        }
    }
}
