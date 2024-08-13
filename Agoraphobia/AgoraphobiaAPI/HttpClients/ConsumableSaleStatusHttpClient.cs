using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class ConsumableSaleStatusHttpClient : HttpClientBase
    {
        public static async Task AddItem(int playerId, int consumableId, int roomId, int merchantId)
        {
            var request = new ConsumableSaleStatusRequestDto()
            {
                PlayerId = playerId,
                RoomId = roomId,
                ConsumableId = consumableId,
                MerchantId = merchantId
            };
            var json = JsonConvert.SerializeObject(request).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}roomMerchantConsumableSaleStatus", stringContent);
            response.EnsureSuccessStatusCode();
        }
        public static async Task RemoveItem(int playerId, int consumableId, int roomId, int merchantId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantConsumableSaleStatus/{playerId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert.DeserializeObject<List<RoomMerchantConsumableSaleStatus>>(consumablesJson);
            if (consumables is null)
                throw new ArgumentException("Player not found");
            var consumableSaleStatus = consumables.Find(x =>
                x.ConsumableId == consumableId && x.RoomId == roomId && x.MerchantId == merchantId);
            if (consumableSaleStatus is null)
                throw new ArgumentException("Consumable not found");
            var response = await HttpClient
                .DeleteAsync($"{ROUTE}roomMerchantConsumableSaleStatus/{consumableSaleStatus.Id}");
            response.EnsureSuccessStatusCode();
        }
        public static async Task CopyConsumables(int playerId, int roomId, int merchantId)
        {
            var statusesResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantConsumableSaleStatus/{playerId}");
            statusesResp.EnsureSuccessStatusCode();
            var statusesJson = await statusesResp.Content.ReadAsStringAsync();
            var statuses = JsonConvert.DeserializeObject<List<RoomMerchantConsumableSaleStatus>>(statusesJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId);
            if (statuses.Any())
                return;
            var salesResp = await HttpClient.GetAsync($"{ROUTE}consumableSales/{merchantId}");
            salesResp.EnsureSuccessStatusCode();
            var salesJson = await salesResp.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<ConsumableSale>>(salesJson)!;
            foreach (var sale in sales)
            {
                for (var i = 0; i < sale.Quantity; i++)
                    await AddItem(playerId, sale.ConsumableId, roomId, merchantId);
            }
        }
        public static async Task<List<RoomMerchantConsumableSaleStatus>> GetConsumables(int playerId, int roomId, int merchantId)
        {
            var consumablesResp = await HttpClient
                .GetAsync($"{ROUTE}roomMerchantConsumableSaleStatus/{playerId}");
            consumablesResp.EnsureSuccessStatusCode();
            var consumablesJson = await consumablesResp.Content.ReadAsStringAsync();
            var consumables = JsonConvert
                .DeserializeObject<List<RoomMerchantConsumableSaleStatus>>(consumablesJson)!
                .Where(x => x.RoomId == roomId && x.MerchantId == merchantId).ToList();
            return consumables;
        }
    }
}
