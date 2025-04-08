using AppDevAssignment.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDevAssignment.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}