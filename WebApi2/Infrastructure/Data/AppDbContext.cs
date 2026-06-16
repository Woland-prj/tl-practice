using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options )
    {
    }

    public AppDbContext()
    {
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        if ( optionsBuilder.IsConfigured )
        {
            return;
        }

        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory() )
            .AddJsonFile( "appsettings.json" )
            .AddJsonFile( "appsettings.Development.json", optional: true )
            .Build();

        string? connectionString = config.GetConnectionString( "DefaultConnection" );
        optionsBuilder.UseNpgsql( connectionString );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( AppDbContext ).Assembly );
    }
}