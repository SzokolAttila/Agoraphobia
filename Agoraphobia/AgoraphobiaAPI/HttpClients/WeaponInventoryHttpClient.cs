using System.Text;
using AgoraphobiaAPI.Dtos.WeaponInventory;
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
    }
}
