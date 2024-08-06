using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomEnemyStatus
{
    public class RoomEnemyStatusDto
    {
        public int RoomId { get; set; }
        public int PlayerId { get; set; }
        public double EnemyHp { get; set; }
    }
}
