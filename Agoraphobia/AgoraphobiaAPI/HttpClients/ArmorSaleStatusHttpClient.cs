using AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class ArmorSaleStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int armorId, int roomId, int merchantId)
        {
            var request = new ArmorSaleStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                ArmorId = armorId,
                MerchantId = merchantId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomMerchantArmorSaleStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int armorId, int roomId, int merchantId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantArmorSaleStatus/{playerId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert.DeserializeObject<List<RoomMerchantArmorSaleStatus>>(armorsJson);
            if (armors is null)
                throw new ArgumentException("Player not found");
            var armorSaleStatus = armors.Find(x =>
                x.ArmorId == armorId && x.RoomId == roomId && x.MerchantId == merchantId);
            if (armorSaleStatus is null)
                throw new ArgumentException("Armor not found");
            var response = await HttpClient
                .DeleteAsync($"{ROUTE}roomMerchantArmorSaleStatus/{armorSaleStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task CopyArmors(int playerId, int roomId, int merchantId)
        {
            var statusesResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantArmorSaleStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomMerchantArmorSaleStatus>>(statusesJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId);
            if (statuses.Any())
                return;
            var salesResp = await HttpClient.GetAsync($"{ROUTE}armorSales/{merchantId}");
            salesResp.EnsureSuccessStatusCode();
            var salesJson = await salesResp.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<ArmorSale>>(salesJson)!;
            foreach (var sale in sales)
            {
                for (var i = 0; i < sale.Quantity; i++)
                    await AddItem(playerId, sale.ArmorId, roomId, merchantId);
            }
        }
        public static async Task<List<RoomMerchantArmorSaleStatus>> GetArmors(int playerId, int roomId, int merchantId)
        {
            var armorsResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantArmorSaleStatus/{playerId}");
            armorsResp.EnsureSuccessStatusCode();
            var armorsJson = await armorsResp.Content.ReadAsStringAsync();
            var armors = JsonConvert
                .DeserializeObject<List<RoomMerchantArmorSaleStatus>>(armorsJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId).ToList();
            return armors;
        }
    }
}
