using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomConsumableLootStatusRepository
    {
        public Task<List<RoomConsumableLootStatus>> GetRoomConsumableLootStatusesAsync(int playerId);
    }
}
