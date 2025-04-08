using Microsoft.AspNetCore.Mvc;
using AppDevAssignment.DTOs;
using AppDevAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace AppDevAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        // GET: api/Banners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetBanners()
        {
            var bannerDTOs = await _bannerService.GetAllBannersAsync();
            return Ok(bannerDTOs);
        }

        // GET: api/Banners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BannerDTO>> GetBanner(int id)
        {
            var bannerDTO = await _bannerService.GetBannerByIdAsync(id);

            if (bannerDTO == null)
            {
                return NotFound();
            }

            return Ok(bannerDTO);
        }


        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetActiveBanners()
        {
            var activeBanners = await _bannerService.GetActiveBannersAsync();
            return Ok(activeBanners);
        }

        // GET: api/Banners/company/5/active
        [HttpGet("company/{companyId}/active")]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetActiveBannersByCompany(int companyId)
        {
            var activeBanners = await _bannerService.GetActiveBannersByCompanyIdAsync(companyId);
            return Ok(activeBanners);
        }

        // PUT: api/Banners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanner(int id, BannerDTO bannerDTO)
        {
            if (id != bannerDTO.BannerId)
            {
                return BadRequest();
            }

            try
            {
                await _bannerService.UpdateBannerAsync(bannerDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BannerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Banners
        [HttpPost]
        public async Task<ActionResult<BannerDTO>> PostBanner(BannerDTO bannerDTO)
        {
            await _bannerService.AddBannerAsync(bannerDTO);
            return CreatedAtAction("GetBanner", new { id = bannerDTO.BannerId }, bannerDTO);
        }

        // DELETE: api/Banners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var bannerDTO = await _bannerService.GetBannerByIdAsync(id);
            if (bannerDTO == null)
            {
                return NotFound();
            }

            await _bannerService.DeleteBannerAsync(id);
            return NoContent();
        }

        private async Task<bool> BannerExists(int id)
        {
            var banner = await _bannerService.GetBannerByIdAsync(id);
            return banner != null;
        }
    }
}