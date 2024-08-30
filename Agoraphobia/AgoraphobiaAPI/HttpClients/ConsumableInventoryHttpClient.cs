using System.Net.Http;
using System.Text;
using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class ConsumableInventoryHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int consumableId)
        {
            var content = new ConsumableInventoryRequestDto()
            {
                PlayerId = playerId,
                ConsumableId = consumableId
            };
            var json = JsonConvert.SerializeObject(content).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}consumableInventories", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int consumableId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}consumableInventories/{playerId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert.DeserializeObject<List<ConsumableInventory>>(consumablesJson);
            if (consumables is null)
                throw new ArgumentException("Player not found");
            var consumableInventory = consumables.Find(x => x.ConsumableId == consumableId);
            if (consumableInventory is null)
                throw new ArgumentException("Consumable not found");
            var response = await HttpClient.DeleteAsync($"{ROUTE}consumableInventories/{consumableInventory.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
