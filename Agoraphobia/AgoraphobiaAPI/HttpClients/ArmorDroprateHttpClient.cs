using AgoraphobiaLibrary.JoinTables.Armors;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class ArmorDroprateHttpClient : HttpClientBase
    {
        public static async Task DropArmors(int enemyId, int playerId, int roomId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}armorDroprates/{enemyId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert.DeserializeObject<List<ArmorDroprate>>(armorsJson)!.ToList();
            foreach (var armor in armors)
            {
                if (Random.Shared.NextDouble() <= armor.Droprate)
                    await ArmorLootStatusHttpClient.AddItem(playerId, armor.ArmorId, roomId);
            }
        }
    }
}
