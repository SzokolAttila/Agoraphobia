using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class RoomWeaponLootStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int weaponId, int roomId)
        {
            var request = new WeaponLootStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                WeaponId = weaponId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomWeaponLootStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int weaponId, int roomId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}roomWeaponLootStatus/{playerId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson = await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<RoomWeaponLootStatus>>(weaponsJson);
            if (weapons is null)
                throw new ArgumentException("Player not found");
            var weaponLootStatus = weapons.Find(x => x.WeaponId == weaponId && x.RoomId == roomId);
            if (weaponLootStatus is null)
                throw new ArgumentException("Weapon not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}roomWeaponLootStatus/{weaponLootStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task EnterRoom(int playerId, int roomId)
        {
            var statusesResp = await HttpClient.GetAsync($"{ROUTE}roomWeaponLootStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomWeaponLootStatus>>(statusesJson)!.Where(x => x.RoomId == roomId);
            if (statuses.Any())
                return;
            var lootsResp = await HttpClient.GetAsync($"{ROUTE}weaponLoots/{roomId}");
            lootsResp.EnsureSuccessStatusCode();
            var lootsJson = await lootsResp.Content.ReadAsStringAsync();
            var loots = JsonConvert.DeserializeObject<List<WeaponLoot>>(lootsJson)!;
            foreach (var loot in loots)
            {
                for (var i = 0; i < loot.Quantity; i++)
                    await AddItem(playerId, loot.WeaponId, roomId);
            }
        }
        public static async Task<List<RoomWeaponLootStatus>> GetWeapons(int playerId, int roomId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}roomWeaponLootStatus/{playerId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson = await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<RoomWeaponLootStatus>>(weaponsJson)!
                .Where(x => x.RoomId == roomId).ToList();
            return weapons;
        }
    }
}
