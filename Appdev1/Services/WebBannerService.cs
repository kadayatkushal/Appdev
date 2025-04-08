using AppDevAssignment.DTOs;
using AppDevAssignment.Entities;
using AppDevAssignment.Repository;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppDevAssignment.Services
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        // Get all banners
        public async Task<IEnumerable<BannerDTO>> GetAllBannersAsync()
        {
            var banners = await _bannerRepository.GetAllAsync();
            return banners.Select(b => new BannerDTO
            {
                BannerId = b.BannerId,
                Title = b.Title,
                Description = b.Description,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                CompanyId = b.CompanyId
            });
        }

        // Get banner by Id
        public async Task<BannerDTO?> GetBannerByIdAsync(int id)
        {
            var banner = await _bannerRepository.GetByIdAsync(id);
            if (banner == null)
                return null;

            return new BannerDTO
            {
                BannerId = banner.BannerId,
                Title = banner.Title,
                Description = banner.Description,
                StartDate = banner.StartDate,
                EndDate = banner.EndDate,
                CompanyId = banner.CompanyId
            };
        }

        // Get active banners based on the current date
        public async Task<IEnumerable<BannerDTO>> GetActiveBannersAsync()
        {
            var currentDate = DateTime.UtcNow;

            var banners = await _bannerRepository.GetAllAsync();
            var activeBanners = banners
                .Where(b => b.StartDate <= currentDate && b.EndDate >= currentDate) // Active banners
                .Select(b => new BannerDTO
                {
                    BannerId = b.BannerId,
                    Title = b.Title,
                    Description = b.Description,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CompanyId = b.CompanyId
                });

            return activeBanners;
        }

        // Get active banners for a specific company based on the current date
        public async Task<IEnumerable<BannerDTO>> GetActiveBannersByCompanyIdAsync(int companyId)
        {
            var currentDate = DateTime.UtcNow;

            var banners = await _bannerRepository.GetAllAsync();
            var activeBannersByCompany = banners
                .Where(b => b.CompanyId == companyId && b.StartDate <= currentDate && b.EndDate >= currentDate)
                .Select(b => new BannerDTO
                {
                    BannerId = b.BannerId,
                    Title = b.Title,
                    Description = b.Description,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CompanyId = b.CompanyId
                });

            return activeBannersByCompany;
        }

        // Add banner with simplified business logic
        public async Task AddBannerAsync(BannerDTO bannerDTO)
        {
            // Validate banner dates
            if (bannerDTO.StartDate >= bannerDTO.EndDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
            }

            // Create the banner
            var banner = new Banner
            {
                Title = bannerDTO.Title,
                Description = bannerDTO.Description,
                StartDate = bannerDTO.StartDate,
                EndDate = bannerDTO.EndDate,
                CompanyId = bannerDTO.CompanyId
            };

            await _bannerRepository.AddAsync(banner);
            bannerDTO.BannerId = banner.BannerId; // Set the BannerId after saving to the DB
        }

        // Update banner with simplified business logic
        public async Task UpdateBannerAsync(BannerDTO bannerDTO)
        {
            // Fetch the existing banner from the database
            var existingBanner = await _bannerRepository.GetByIdAsync(bannerDTO.BannerId);
            if (existingBanner == null)
            {
                throw new ArgumentException("Banner does not exist.");
            }

            // Validate banner dates
            if (bannerDTO.StartDate >= bannerDTO.EndDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
            }

            // Update the banner
            existingBanner.Title = bannerDTO.Title;
            existingBanner.Description = bannerDTO.Description;
            existingBanner.StartDate = bannerDTO.StartDate;
            existingBanner.EndDate = bannerDTO.EndDate;
            existingBanner.CompanyId = bannerDTO.CompanyId;

            await _bannerRepository.UpdateAsync(existingBanner);
        }

        // Delete banner
        public async Task DeleteBannerAsync(int id)
        {
            // Ensure the banner exists
            var banner = await _bannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw new ArgumentException("Banner does not exist.");
            }

            await _bannerRepository.DeleteAsync(id);
        }
    }
}