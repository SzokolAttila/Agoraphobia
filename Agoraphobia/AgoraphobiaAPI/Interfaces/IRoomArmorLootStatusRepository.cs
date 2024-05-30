using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomArmorLootStatusRepository
    {
        public Task<List<RoomArmorLootStatus>> GetRoomArmorLootStatusesAsync(int playerId);
        public Task<RoomArmorLootStatus> CreateAsync(RoomArmorLootStatus status);
        public Task<RoomArmorLootStatus?> AddOneAsync(ArmorLootStatusRequestDto update);
    }
}
