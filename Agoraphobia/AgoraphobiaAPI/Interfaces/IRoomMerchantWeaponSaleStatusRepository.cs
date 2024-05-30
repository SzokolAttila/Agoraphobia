using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomMerchantWeaponSaleStatusRepository
    {
        public Task<List<RoomMerchantWeaponSaleStatus>> GetWeaponSalesAsync(int playerId);

    }
}
