using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IMerchantRepository
    {
        Task<List<Merchant>> GetAllAsync();
        Task<Merchant?> GetByIdAsync(int id);
        Task<Merchant> CreateAsync(Merchant merchant);

    }
}
