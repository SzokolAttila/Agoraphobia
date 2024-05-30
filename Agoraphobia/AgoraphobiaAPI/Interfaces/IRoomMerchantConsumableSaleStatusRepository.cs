using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantConsumableSaleStatusRepository
    {
        public Task<List<RoomMerchantConsumableSaleStatus>> GetConsumableSalesAsync(int playerId);

    }
}
