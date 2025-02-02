using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Movies.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MovieDbContext>
{
    public MovieDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
        
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Movies.API"); // 🔹 Ajuste o caminho base
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) 
            .AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true) 
            .Build();
        
        var connectionString = configuration.GetConnectionString("CatalogDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is null or empty!");
        }

        optionsBuilder.UseNpgsql(connectionString);
        
        return new MovieDbContext(optionsBuilder.Options);
    }
}