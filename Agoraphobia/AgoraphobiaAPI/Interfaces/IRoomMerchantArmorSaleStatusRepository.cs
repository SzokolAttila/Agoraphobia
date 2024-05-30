using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantArmorSaleStatusRepository
    {
        public Task<List<RoomMerchantArmorSaleStatus>> GetArmorSalesAsync(int playerId);
        public Task<RoomMerchantArmorSaleStatus> CreateAsync(RoomMerchantArmorSaleStatus status);
        public Task<RoomMerchantArmorSaleStatus?> AddOneAsync(ArmorSaleStatusRequestDto update);
        public Task<RoomMerchantArmorSaleStatus?> DeleteAsync(RoomMerchantArmorSaleStatus status);
        public Task<RoomMerchantArmorSaleStatus?> RemoveOneAsync(ArmorSaleStatusRequestDto update);

    }
}
