using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext( string[] args )
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath( Path.Combine( Directory.GetCurrentDirectory(), "..", "WebApi" ) )
            .AddJsonFile( "appsettings.json" )
            .AddJsonFile( "appsettings.Development.json", optional: true )
            .Build();

        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        string? connectionString = config.GetConnectionString( "DefaultConnection" );
        optionsBuilder.UseNpgsql( connectionString );

        return new AppDbContext( optionsBuilder.Options );
    }
}