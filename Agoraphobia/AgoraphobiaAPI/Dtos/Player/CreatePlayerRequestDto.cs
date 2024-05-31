namespace AgoraphobiaAPI.Dtos.Player;

public class CreatePlayerRequestDto
{
    public int AccountId { get; set; }
    public int RoomId { get; set; }
    public int SlotId { get; set; }
}