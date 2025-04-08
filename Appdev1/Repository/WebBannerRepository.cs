using AppDevAssignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDevAssignment.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly Appdev1DbContext _context;

        public BannerRepository(Appdev1DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            return await _context.Banners.Include(b => b.Company).ToListAsync();
        }

        public async Task<Banner?> GetByIdAsync(int id)
        {
            return await _context.Banners.Include(b => b.Company).FirstOrDefaultAsync(b => b.BannerId == id);
        }

        public async Task AddAsync(Banner banner)
        {
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Banner banner)
        {
            _context.Banners.Update(banner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
