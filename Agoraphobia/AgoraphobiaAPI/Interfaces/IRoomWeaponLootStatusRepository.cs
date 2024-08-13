using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomWeaponLootStatusRepository
    {
        public Task<List<RoomWeaponLootStatus>> GetRoomWeaponLootStatusesAsync(int playerId);
        public Task<RoomWeaponLootStatus> CreateAsync(RoomWeaponLootStatus status);
        public Task<RoomWeaponLootStatus?> AddOneAsync(WeaponLootStatusRequestDto update);
        public Task<RoomWeaponLootStatus?> GetByIdAsync(int weaponLootStatusId);
        public Task<RoomWeaponLootStatus?> RemoveOneAsync(WeaponLootStatusRequestDto update);
    }
}
