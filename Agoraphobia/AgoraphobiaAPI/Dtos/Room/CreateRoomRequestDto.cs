namespace AgoraphobiaAPI.Dtos.Room
{
    public class CreateRoomRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrientationId { get; set; }
        public int EnemyId { get; set; }
    }
}
