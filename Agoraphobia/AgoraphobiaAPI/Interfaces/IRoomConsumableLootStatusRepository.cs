using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomConsumableLootStatusRepository
    {
        public Task<List<RoomConsumableLootStatus>> GetRoomConsumableLootStatusesAsync(int playerId);
        public Task<RoomConsumableLootStatus> CreateAsync(RoomConsumableLootStatus status);
        public Task<RoomConsumableLootStatus?> AddOneAsync(ConsumableLootStatusRequestDto update);
    }
}
