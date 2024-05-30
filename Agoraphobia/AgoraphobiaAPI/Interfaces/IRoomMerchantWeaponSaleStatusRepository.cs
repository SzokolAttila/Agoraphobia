using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantWeaponSaleStatusRepository
    {
        public Task<List<RoomMerchantWeaponSaleStatus>> GetWeaponSalesAsync(int playerId);
        public Task<RoomMerchantWeaponSaleStatus> CreateAsync(RoomMerchantWeaponSaleStatus status);
        public Task<RoomMerchantWeaponSaleStatus?> AddOneAsync(WeaponSaleStatusRequestDto update);
    }
}
