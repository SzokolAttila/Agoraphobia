using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary.Exceptions.Account;
using AgoraphobiaLibrary;
using Newtonsoft.Json;
using System.Text;
using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Dtos.Room;
using System.IdentityModel.Tokens.Jwt;

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

        public async Task<Player> AddNewPlayer(int accountId, int roomId, int slotId)
        {
            var accounts = await _httpClient.GetAsync($"{ROUTE}accounts");
            var account = accounts.Content.ReadFromJsonAsync<List<AccountDto>>().Result!.Find(x => x.Id == accountId);
            if (account is null)
                throw new ArgumentException("Account not found");
            var rooms = await _httpClient.GetAsync($"{ROUTE}rooms/{roomId}");
            var roomsResp = await rooms.Content.ReadAsStringAsync();
            //var room = rooms.Content.ReadFromJsonAsync<List<Room>>().Result!.Find(x => x.Id == roomId);
            var room = JsonConvert.DeserializeObject<Room>(roomsResp);
            
            if (room is null)
                throw new ArgumentException("Room not found");
            var player = new Player(accountId, roomId, slotId);
            player.Room = room;
            var content = new StringContent(JsonConvert.SerializeObject(new CreatePlayerRequestDto()
            {
                AccountId = accountId,
                RoomId = roomId,
                SlotId = slotId
            }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ROUTE}players", content);
            response.EnsureSuccessStatusCode();
            return player;
        }
    }
}
