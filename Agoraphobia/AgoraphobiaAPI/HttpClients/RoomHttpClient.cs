using AgoraphobiaLibrary;
using Newtonsoft.Json;
using static AgoraphobiaLibrary.Room;

namespace AgoraphobiaAPI.HttpClients
{
    public class RoomHttpClient : HttpClientBase
    {
        public static async Task<List<Room>> RoomsByOrientation(RoomOrientation orientation)
        {
            var roomsResp = await HttpClient.GetAsync($"{ROUTE}rooms");
            string roomsJson = await roomsResp.Content.ReadAsStringAsync();
            List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(roomsJson);
            return rooms.Where(x => x.Orientation == orientation).ToList();
        }
    }
}
