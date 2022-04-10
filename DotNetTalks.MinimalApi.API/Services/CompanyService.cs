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

        public async Task<List<Company>> GetAllCompanies()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
