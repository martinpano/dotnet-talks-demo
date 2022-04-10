using DotNetTalks.MinimalApi.API.Db;

namespace DotNetTalks.MinimalApi.API.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllCompanies();
    }
}
