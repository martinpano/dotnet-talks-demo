using Microsoft.EntityFrameworkCore;

namespace DotNetTalks.MinimalApi.API.Db
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder moduleBuilder)
        {
            moduleBuilder.Entity<Company>()
                .HasMany(x => x.Employees)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);


            moduleBuilder.Entity<Company>()
                .HasData(
                new Company
                {
                    Id = 1,
                    Name = "Endava",
                    Address = "N/A",
                    City = "Skopje",
                    NumberOfEmployees = 450
                }
                );

            moduleBuilder.Entity<Employee>()
                .HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Martin Panovski",
                    YearsOfExperiance = 4,
                    CompanyId = 1
                });
        }
    }
}
