using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomEnemyStatus
{
    public class RoomEnemyStatusDto
    {
        public RoomDto Room { get; set; } = new();
        public double EnemyHp { get; set; }
    }
}
