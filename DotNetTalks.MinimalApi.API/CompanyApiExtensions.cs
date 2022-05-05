using DotNetTalks.MinimalApi.API.Db;
using DotNetTalks.MinimalApi.API.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetTalks.MinimalApi.API
{
    public static class CompanyApiExtensions
    {
        public static WebApplication MapCompanyApi(this WebApplication app)
        {
            app.MapGet("/companies", async (ICompanyService companyService) =>
            {
                var companies = await companyService.GetAllCompanies();
                return companies;
            });

            app.MapGet("/companies/{id:int}", async (int id, CompanyDbContext db) =>
            {
                var company = await db.Companies.FindAsync(id);
                return company;
            });

            app.MapPost("/companies", async (Company company, ICompanyService companyService) =>
            {
                var result = await companyService.AddNewCompany(company);
                if (result > 0)
                    return Results.Created("New company created.", company.Id);
                return Results.Problem(statusCode: 500, detail: "Adding new company failed!");
            });


            app.MapPut("/companies/{id:int}", async (int id, Company companyUpdate, ICompanyService companyService) =>
            {
                var company = await companyService.GetCompanyById(id);
                if (company is null)
                    return Results.NotFound();

                var result = await companyService.UpdateCompany(company);
                if (result > 0)
                    return Results.Ok("Company updated successfully");

                return Results.Problem(statusCode: 500, detail: "Updateing new company failed!");
            });

            app.MapDelete("/companies/{id:int}", async (int id, ICompanyService companyService) =>
            {
                var result = await companyService.DeleteCompany(id);
                if (result > 0) return Results.Ok("Company deleted successfully!");
                return Results.NotFound();
            });
            return app;
        }

        public static IServiceCollection UseCompanyApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<ICompanyService, CompanyService>();

            return services;
        }
    }
}
