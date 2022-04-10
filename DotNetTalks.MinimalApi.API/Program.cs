using DotNetTalks.MinimalApi.API.Db;
using DotNetTalks.MinimalApi.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompanyDbContext>(x => x.UseSqlServer("Server=.;Database=CompanyDatabase;Trusted_Connection=True"));
builder.Services.AddTransient<ICompanyService, CompanyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/companies", async (ICompanyService companyService) =>
{
    var companies = await companyService.GetAllCompanies();
    return companies;
});

app.MapGet("/companies/{id}", async (int id, CompanyDbContext db) =>
{
    var company = await db.Companies.FindAsync(id);
    return company;
});

app.MapPost("/companies", async (Company company, CompanyDbContext db) =>
{
    if(company != null)
    {
        await db.Companies.AddAsync(company);
        await db.SaveChangesAsync();
    }
    return Results.Created("New company created.", company.Id);
});


app.MapPut("/companies/{id}", async (int id, Company companyUpdate, CompanyDbContext db) =>
{
    var company = await db.Companies.FindAsync(id);

    if (company is null) return Results.NotFound();

    company.Name = companyUpdate.Name;
    company.Address = companyUpdate.Address;
    company.NumberOfEmployees = companyUpdate.NumberOfEmployees;
    company.City = companyUpdate.City;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/companies/{id}", async (int id, CompanyDbContext db) =>
{
    if (await db.Companies.FindAsync(id) is Company company)
    {
        db.Companies.Remove(company);
        await db.SaveChangesAsync();
        return Results.Ok(company);
    }

    return Results.NotFound();
});


app.Run();
