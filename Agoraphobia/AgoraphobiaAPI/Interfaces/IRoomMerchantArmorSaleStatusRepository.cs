using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantArmorSaleStatusRepository
    {
        public Task<List<RoomMerchantArmorSaleStatus>> GetArmorSalesAsync(int playerId);

    }
}
