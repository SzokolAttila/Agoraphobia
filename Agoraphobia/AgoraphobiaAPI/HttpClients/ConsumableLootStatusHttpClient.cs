using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class ConsumableLootStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int consumableId, int roomId)
        {
            var request = new ConsumableLootStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                ConsumableId = consumableId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomConsumableLootStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int consumableId, int roomId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}roomConsumableLootStatus/{playerId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert.DeserializeObject<List<RoomConsumableLootStatus>>(consumablesJson);
            if (consumables is null)
                throw new ArgumentException("Player not found");
            var consumableLootStatus = consumables.Find(x => x.ConsumableId == consumableId && x.RoomId == roomId);
            if (consumableLootStatus is null)
                throw new ArgumentException("Consumable not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}roomConsumableLootStatus/{consumableLootStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task CopyConsumables(int playerId, int roomId)
        {
            var statusesResp = await HttpClient.GetAsync($"{ROUTE}roomConsumableLootStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomConsumableLootStatus>>(statusesJson)!.Where(x => x.RoomId == roomId);
            if (statuses.Any())
                return;
            var lootsResp = await HttpClient.GetAsync($"{ROUTE}consumableLoots/{roomId}");
            lootsResp.EnsureSuccessStatusCode();
            var lootsJson = await lootsResp.Content.ReadAsStringAsync();
            var loots = JsonConvert.DeserializeObject<List<ConsumableLoot>>(lootsJson)!;
            foreach (var loot in loots)
            {
                for (var i = 0; i < loot.Quantity; i++)
                    await AddItem(playerId, loot.ConsumableId, roomId);
            }
        }
        public static async Task<List<RoomConsumableLootStatus>> GetConsumables(int playerId, int roomId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}roomConsumableLootStatus/{playerId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert.DeserializeObject<List<RoomConsumableLootStatus>>(consumablesJson)!
                .Where(x => x.RoomId == roomId).ToList();
            return consumables;
        }
    }
}
