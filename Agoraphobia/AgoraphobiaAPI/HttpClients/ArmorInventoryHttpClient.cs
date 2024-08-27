using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaLibrary.JoinTables.Armors;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class ArmorInventoryHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int armorId)
        {
            var content = new ArmorInventoryRequestDto()
            {
                PlayerId = playerId,
                ArmorId = armorId
            };
            var json = JsonConvert.SerializeObject(content).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}armorInventories", stringContent);
            response.EnsureSuccessStatusCode();
        }

        public static async Task RemoveItem(int playerId, int armorId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}armorInventories/{playerId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert.DeserializeObject<List<ArmorInventory>>(armorsJson);
            if (armors is null)
                throw new ArgumentException("Player not found");
            var armorInventory = armors.Find(x => x.ArmorId == armorId);
            if (armorInventory is null)
                throw new ArgumentException("Armor not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}armorInventories/{armorInventory.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
