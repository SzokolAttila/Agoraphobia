using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomArmorLootStatusRepository
    {
        public Task<List<RoomArmorLootStatus>> GetRoomArmorLootStatusesAsync(int playerId);
    }
}
