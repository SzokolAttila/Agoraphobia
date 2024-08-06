using System.Runtime.InteropServices;
using System.Text;
using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public static class RoomEnemyStatusHttpClient
    {
        private static readonly HttpClient HttpClient = new();
        private const string ROUTE = "http://localhost:5172/agoraphobia/";

        private static async Task CreateStatus(int playerId, int roomId, double health)
        {
            var content = new CreateRoomEnemyStatusDto()
            {
                RoomId = roomId,
                EnemyHp = health,
                PlayerId = playerId
            };
            var contentJson = new StringContent(JsonConvert.SerializeObject(content),
                Encoding.UTF8, "application/json");
            var statusResp = await
                HttpClient.PostAsync($"{ROUTE}roomEnemyStatus", contentJson);
            statusResp.EnsureSuccessStatusCode();
        }
        public static async Task UpdateEnemyHealth(int playerId, int roomId, double health)
        {
            var content = new CreateRoomEnemyStatusDto()
            {
                RoomId = roomId,
                EnemyHp = health,
                PlayerId = playerId
            };
            var contentJson = new StringContent(JsonConvert.SerializeObject(content), 
                Encoding.UTF8, "application/json");
            var statusResp = await 
                HttpClient.PutAsync($"{ROUTE}roomEnemyStatus", contentJson);
            if (!statusResp.IsSuccessStatusCode)
                await CreateStatus(playerId, roomId, health);
        }
    }
}
