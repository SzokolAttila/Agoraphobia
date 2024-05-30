using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomConsumableLootStatus
{
    public class RoomConsumableLootStatusDto
    {
        public int PlayerId { get; set; }
        public RoomDto Room { get; set; } = new();
        public ConsumableDto Consumable { get; set; } = new();
        public int Quantity { get; set; }
    }
}
