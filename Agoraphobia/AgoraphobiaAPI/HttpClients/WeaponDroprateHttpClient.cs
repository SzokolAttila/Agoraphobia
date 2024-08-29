using AgoraphobiaLibrary.JoinTables.Weapons;
using Newtonsoft.Json;

namespace AgoraphobiaAPI.HttpClients
{
    public class WeaponDroprateHttpClient : HttpClientBase
    {
        public static async Task DropWeapons(int enemyId, int playerId, int roomId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}weaponDroprates/{enemyId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson= await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<WeaponDroprate>>(weaponsJson)!.ToList();
            foreach (var weapon in weapons)
            {
                if(Random.Shared.NextDouble() <= weapon.Droprate)
                    await RoomWeaponLootStatusHttpClient.AddItem(playerId, weapon.WeaponId, roomId);
            }
        }
    }
}
