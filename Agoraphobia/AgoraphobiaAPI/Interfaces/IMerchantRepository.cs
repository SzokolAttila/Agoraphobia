using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IMerchantRepository
    {
        Task<List<Merchant>> GetAllAsync();
    }
}
