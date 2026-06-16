using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.HasKey( e => e.Id );

        builder.Property( e => e.Name )
            .IsRequired()
            .HasMaxLength( 200 );

        builder.Property( e => e.Country )
            .IsRequired()
            .HasMaxLength( 100 );

        builder.Property( e => e.City )
            .IsRequired()
            .HasMaxLength( 100 );

        builder.Property( e => e.Address )
            .IsRequired()
            .HasMaxLength( 300 );

        builder.Property( e => e.Latitude )
            .HasColumnType( "decimal(9,6)" );

        builder.Property( e => e.Longitude )
            .HasColumnType( "decimal(9,6)" );
    }
}