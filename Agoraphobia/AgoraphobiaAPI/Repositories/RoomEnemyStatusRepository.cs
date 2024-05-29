using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomEnemyStatusRepository : IRoomEnemyStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomEnemyStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomEnemyStatus>> GetRoomStatusesAsync(int playerId)
        {
            return await _context.RoomEnemyStatus
                .Where(x => x.PlayerId == playerId).ToListAsync();
        }
    }
}
