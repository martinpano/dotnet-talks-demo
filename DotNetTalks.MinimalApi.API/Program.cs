using DotNetTalks.MinimalApi.API;
using DotNetTalks.MinimalApi.API.Db;
using DotNetTalks.MinimalApi.API.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CompanyDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));


// Use all the registrations
builder.Services.UseCompanyApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map all the endpoints
app.MapCompanyApi();

app.Run();
