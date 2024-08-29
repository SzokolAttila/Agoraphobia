using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class ConsumableDroprateHttpClient : HttpClientBase
    {
        public static async Task DropConsumables(int enemyId, int playerId, int roomId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}consumableDroprates/{enemyId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert.DeserializeObject<List<ConsumableDroprate>>(consumablesJson)!.ToList();
            foreach (var consumable in consumables)
            {
                if (Random.Shared.NextDouble() <= consumable.Droprate)
                    await ConsumableLootStatusHttpClient.AddItem(playerId, consumable.ConsumableId, roomId);
            }
        }
    }
}
