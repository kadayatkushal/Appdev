using AppDevAssignment.DTOs;

namespace AppDevAssignment.Services
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerDTO>> GetAllBannersAsync();
        Task<BannerDTO?> GetBannerByIdAsync(int id);
        Task<IEnumerable<BannerDTO>> GetActiveBannersAsync();
        Task<IEnumerable<BannerDTO>> GetActiveBannersByCompanyIdAsync(int companyId);
        Task AddBannerAsync(BannerDTO banner);
        Task UpdateBannerAsync(BannerDTO banner);
        Task DeleteBannerAsync(int id);
    }
}