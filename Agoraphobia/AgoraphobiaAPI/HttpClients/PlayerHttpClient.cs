using System.ComponentModel;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary.Exceptions.Account;
using AgoraphobiaLibrary;
using System.Text;
using Newtonsoft.Json;
using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Dtos.Room;
using System.IdentityModel.Tokens.Jwt;
using AgoraphobiaAPI.Mappers;

namespace AgoraphobiaAPI.HttpClients
{
    public class PlayerHttpClient
    {
        private readonly HttpClient _httpClient;
        public PlayerHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string ROUTE = "http://localhost:5172/agoraphobia/";

        public async Task<Player> AddNewPlayer(int accountId, int slotId)
        {
            var accountResp = await _httpClient.GetAsync($"{ROUTE}accounts/{accountId}");
            var accountJson = await accountResp.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            if (account is null)
                throw new ArgumentException("Account not found");
            var roomResp = await _httpClient.GetAsync($"{ROUTE}rooms/{1}");
            var roomJson = await roomResp.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<Room>(roomJson);
            if (room is null)
                throw new ArgumentException("Room not found");

            var content = new StringContent(JsonConvert.SerializeObject(new CreatePlayerRequestDto()
            {
                AccountId = accountId,
                RoomId = 1,
                SlotId = slotId
            }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ROUTE}players", content);
            response.EnsureSuccessStatusCode();
            var playerJson = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerJson);
            return player;
        }

        public async Task Save(Player player)
        {
            var playerJson = JsonConvert.SerializeObject(new UpdatePlayerRequestDto()
            { 
                Sanity = player.Sanity, 
                MaxHealth = player.MaxHealth, 
                Health = player.Health, 
                MaxEnergy = player.MaxEnergy, 
                Energy = player.Energy, 
                Attack = player.Attack, 
                Defense = player.Defense,
                DreamCoins = player.DreamCoins, 
                RoomId = player.RoomId, 
            }).Replace("\"", "'");
            var httpContent = new StringContent(playerJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ROUTE}players/{player.Id}", httpContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Player> LoadPlayer(int accountId, int slotId)
        {
            var playersResponse = await _httpClient.GetAsync($"{ROUTE}players");
            var playersJson = await playersResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(playersJson);
            var player = players!.Find(x => x.AccountId == accountId && x.SlotId == slotId);
            if (player is null)
                return await AddNewPlayer(accountId, slotId);
            return player;
        }
    }
}
