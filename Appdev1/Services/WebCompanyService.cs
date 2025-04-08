using AppDevAssignment.DTOs;
using AppDevAssignment.Entities;
using AppDevAssignment.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDevAssignment.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(c => new CompanyDTO
            {
                CompanyId = c.CompanyId,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CompanyDTO?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                return null;

            return new CompanyDTO
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Description = company.Description
            };
        }

        public async Task AddCompanyAsync(CompanyDTO companyDTO)
        {
            var company = new Company
            {
                Name = companyDTO.Name,
                Description = companyDTO.Description
            };

            await _companyRepository.AddAsync(company);
            companyDTO.CompanyId = company.CompanyId;
        }

        public async Task UpdateCompanyAsync(CompanyDTO companyDTO)
        {
            var company = new Company
            {
                CompanyId = companyDTO.CompanyId,
                Name = companyDTO.Name,
                Description = companyDTO.Description
            };

            await _companyRepository.UpdateAsync(company);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _companyRepository.DeleteAsync(id);
        }
    }
}