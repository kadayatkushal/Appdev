using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDevAssignment.DTOs;
using AppDevAssignment.Services;

namespace AppDevAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyDTO companyDTO)
        {
            if (id != companyDTO.CompanyId)
            {
                return BadRequest();
            }

            try
            {
                await _companyService.UpdateCompanyAsync(companyDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CompanyExists(id))
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

        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> PostCompany(CompanyDTO companyDTO)
        {
            await _companyService.AddCompanyAsync(companyDTO);

            return CreatedAtAction("GetCompany", new { id = companyDTO.CompanyId }, companyDTO);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }

        private async Task<bool> CompanyExists(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return company != null;
        }
    }
}