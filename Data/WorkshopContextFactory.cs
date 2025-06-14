using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AutoWorkshopWeb.Models;

namespace AutoWorkshopWeb.Data;

public class WorkshopContextFactory : IDesignTimeDbContextFactory<WorkshopContext>
{
    public WorkshopContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../AutoWorkshopWeb"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<WorkshopContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new WorkshopContext(optionsBuilder.Options);
    }

}