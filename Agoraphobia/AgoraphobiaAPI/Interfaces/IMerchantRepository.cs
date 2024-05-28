using AgoraphobiaAPI.Dtos.Merchant;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IMerchantRepository
    {
        Task<List<Merchant>> GetAllAsync();
        Task<Merchant?> GetByIdAsync(int id);
        Task<Merchant> CreateAsync(Merchant merchant);
        Task<Merchant?> DeleteAsync(int id);
        Task<Merchant?> UpdateAsync(int id, MerchantRequestDto merchantDto);
    }
}
