using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<Room?> CreateAsync(Room room);
    Task<Room?> DeleteAsync(int id);
    Task<Room?> UpdateAsync(int id, CreateRoomRequestDto room);
}