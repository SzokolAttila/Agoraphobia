namespace AgoraphobiaAPI.Dtos.RoomEnemyStatus
{
    public class CreateRoomEnemyStatusDto
    {
        public int PlayerId { get; set; }
        public int RoomId { get; set; }
        public double EnemyHp { get; set; }
    }
}
