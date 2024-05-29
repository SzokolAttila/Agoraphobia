using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomEnemyStatusRepository
    {
        public Task<List<RoomEnemyStatus>> GetRoomStatusesAsync(int playerId);
        public Task<RoomEnemyStatus> CreateRoomStatusAsync(RoomEnemyStatus roomStatus);
        public Task<RoomEnemyStatus?> UpdateRoomStatusAsync(CreateRoomEnemyStatusDto roomStatus);
    }
}
