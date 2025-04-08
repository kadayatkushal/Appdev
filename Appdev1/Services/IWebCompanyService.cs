using AppDevAssignment.DTOs;

namespace AppDevAssignment.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
        Task<CompanyDTO?> GetCompanyByIdAsync(int id);
        Task AddCompanyAsync(CompanyDTO companyDTO);
        Task UpdateCompanyAsync(CompanyDTO companyDTO);
        Task DeleteCompanyAsync(int id);
    }
}
