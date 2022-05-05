using DotNetTalks.MinimalApi.API.Db;
using Microsoft.EntityFrameworkCore;

namespace DotNetTalks.MinimalApi.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyDbContext _context;
        public CompanyService(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewCompany(Company company)
        {
            if(company != null)
            {
                _context.Add(company);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> DeleteCompany(int id)
        {
            var company = await GetCompanyById(id);
            if(company != null)
            {
                _context.Companies.Remove(company);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<List<Company>> GetAllCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.Companies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateCompany(Company company)
        {
            var existingCompany = await _context.Companies.SingleOrDefaultAsync(x => x.Id == company.Id);
            if (existingCompany != null)
            {
                _context.Update(company);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }
    }
}
