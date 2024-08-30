using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaLibrary;
using Newtonsoft.Json;
using System.Text;

namespace AgoraphobiaAPI.HttpClients
{
    public class EffectHttpClient : HttpClientBase
    {
        public static async Task ApplyEffect(int playerId, int consumableId)
        {
            await ConsumableInventoryHttpClient.RemoveItem(playerId, consumableId);
            var consumableResp = await HttpClient
                .GetAsync($"{ROUTE}consumables/{consumableId}");
            consumableResp.EnsureSuccessStatusCode();
            var consumableJson = await consumableResp.Content.ReadAsStringAsync();
            var consumable = JsonConvert.DeserializeObject<Consumable>(consumableJson);
            if (consumable is null)
                throw new ArgumentException("Consumable not found");
            var content = new EffectRequestDto()
            {
                ConsumableId = consumableId,
                PlayerId = playerId
            };
            var json = JsonConvert.SerializeObject(content).Replace("\"", "'");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{ROUTE}effects", stringContent);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DecreaseDuration(int playerId)
        {
            var effectsResp = await HttpClient
                .GetAsync($"{ROUTE}effects/{playerId}");
            effectsResp.EnsureSuccessStatusCode();
            var effectsJson = await effectsResp.Content.ReadAsStringAsync();
            var effects = JsonConvert.DeserializeObject<List<Effect>>(effectsJson);
            if (effects is null)
                throw new ArgumentException("Player not found");
            foreach (var effect in effects)
            {
                var response = await HttpClient.DeleteAsync($"{ROUTE}effects/{effect.Id}");
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task RemoveAllEffects(int playerId)
        {
            var effectsResp = await HttpClient
                .GetAsync($"{ROUTE}effects/{playerId}");
            effectsResp.EnsureSuccessStatusCode();
            var effectsJson = await effectsResp.Content.ReadAsStringAsync();
            var effects = JsonConvert.DeserializeObject<List<Effect>>(effectsJson);
            if (effects is null)
                throw new ArgumentException("Player not found");
            foreach (var effect in effects)
            {
                for (var i = 0; i < effect.CurrentDuration; i++)
                {
                    var response = await HttpClient.DeleteAsync($"{ROUTE}effects/{effect.Id}");
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}
