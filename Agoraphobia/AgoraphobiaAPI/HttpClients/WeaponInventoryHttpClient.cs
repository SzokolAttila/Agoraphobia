using System.Runtime.CompilerServices;
using System.Text;
using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class WeaponInventoryHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int weaponId)
        {
            var content = new WeaponInventoryRequestDto()
            {
                PlayerId = playerId,
                WeaponId = weaponId
            };
            var json = JsonConvert.SerializeObject(content).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}weaponInventories", stringContent);
            response.EnsureSuccessStatusCode();
        }

        public static async Task RemoveItem(int playerId, int weaponId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}weaponInventories/{playerId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson = await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<WeaponInventory>>(weaponsJson);
            if (weapons is null)
                throw new ArgumentException("Player not found");
            var weaponInventory = weapons.Find(x => x.WeaponId == weaponId);
            if (weaponInventory is null)
                throw new ArgumentException("Weapon not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}weaponInventories/{weaponInventory.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
