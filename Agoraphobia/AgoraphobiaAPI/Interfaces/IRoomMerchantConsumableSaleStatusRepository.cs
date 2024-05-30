using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantConsumableSaleStatusRepository
    {
        public Task<List<RoomMerchantConsumableSaleStatus>> GetConsumableSalesAsync(int playerId);
        public Task<RoomMerchantConsumableSaleStatus> CreateAsync(RoomMerchantConsumableSaleStatus status);
        public Task<RoomMerchantConsumableSaleStatus?> AddOneAsync(ConsumableSaleStatusRequestDto update);

    }
}
