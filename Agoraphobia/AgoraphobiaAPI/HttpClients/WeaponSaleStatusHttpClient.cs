using AgoraphobiaLibrary.JoinTables.Rooms;
using Newtonsoft.Json;
using System.Text;
using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.HttpClients
{
    public class WeaponSaleStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int weaponId, int roomId, int merchantId)
        {
            var request = new WeaponSaleStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                WeaponId = weaponId,
                MerchantId = merchantId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomMerchantWeaponSaleStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int weaponId, int roomId, int merchantId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantWeaponSaleStatus/{playerId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson = await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<RoomMerchantWeaponSaleStatus>>(weaponsJson);
            if (weapons is null)
                throw new ArgumentException("Player not found");
            var weaponSaleStatus = weapons.Find(x => 
                x.WeaponId == weaponId && x.RoomId == roomId && x.MerchantId == merchantId);
            if (weaponSaleStatus is null)
                throw new ArgumentException("Weapon not found");
            var response = await HttpClient
                .DeleteAsync($"{ROUTE}roomMerchantWeaponSaleStatus/{weaponSaleStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task CopyWeapons(int playerId, int roomId, int merchantId)
        {
            var statusesResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantWeaponSaleStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomMerchantWeaponSaleStatus>>(statusesJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId);
            if (statuses.Any())
                return;
            var salesResp = await HttpClient.GetAsync($"{ROUTE}weaponSales/{merchantId}");
            salesResp.EnsureSuccessStatusCode();
            var salesJson = await salesResp.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<WeaponSale>>(salesJson)!;
            foreach (var sale in sales)
            {
                for (var i = 0; i < sale.Quantity; i++)
                    await AddItem(playerId, sale.WeaponId, roomId, merchantId);
            }
        }
        public static async Task<List<RoomMerchantWeaponSaleStatus>> GetWeapons (int playerId, int roomId, int merchantId)
        {
            var weaponsResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantWeaponSaleStatus/{playerId}");
            weaponsResp.EnsureSuccessStatusCode();
            var weaponsJson = await weaponsResp.Content.ReadAsStringAsync();
            var weapons = JsonConvert
                .DeserializeObject<List<RoomMerchantWeaponSaleStatus>>(weaponsJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId).ToList();
            return weapons;
        }
    }
}
