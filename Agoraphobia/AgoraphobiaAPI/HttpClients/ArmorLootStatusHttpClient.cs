using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class ArmorLootStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int armorId, int roomId)
        {
            var request = new ArmorLootStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                ArmorId = armorId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomArmorLootStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int armorId, int roomId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}roomArmorLootStatus/{playerId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert.DeserializeObject<List<RoomArmorLootStatus>>(armorsJson);
            if (armors is null)
                throw new ArgumentException("Player not found");
            var armorLootStatus = armors.Find(x => x.ArmorId == armorId && x.RoomId == roomId);
            if (armorLootStatus is null)
                throw new ArgumentException("Armor not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}roomArmorLootStatus/{armorLootStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task CopyArmors(int playerId, int roomId)
        {
            var statusesResp = await HttpClient.GetAsync($"{ROUTE}roomArmorLootStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomArmorLootStatus>>(statusesJson)!.Where(x => x.RoomId == roomId);
            if (statuses.Any())
                return;
            var lootsResp = await HttpClient.GetAsync($"{ROUTE}armorLoots/{roomId}");
            lootsResp.EnsureSuccessStatusCode();
            var lootsJson = await lootsResp.Content.ReadAsStringAsync();
            var loots = JsonConvert.DeserializeObject<List<ArmorLoot>>(lootsJson)!;
            foreach (var loot in loots)
            {
                for (var i = 0; i < loot.Quantity; i++)
                    await AddItem(playerId, loot.ArmorId, roomId);
            }
        }
        public static async Task<List<RoomArmorLootStatus>> GetArmors(int playerId, int roomId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}roomArmorLootStatus/{playerId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert.DeserializeObject<List<RoomArmorLootStatus>>(armorsJson)!
                .Where(x => x.RoomId == roomId).ToList();
            return armors;
        }
    }
}
