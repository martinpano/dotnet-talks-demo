using DotNetTalks.MinimalApi.API.Db;

namespace DotNetTalks.MinimalApi.API.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task<int> AddNewCompany(Company company);
        Task<int> UpdateCompany(Company company);
        Task<int> DeleteCompany(int id);
    }
}
