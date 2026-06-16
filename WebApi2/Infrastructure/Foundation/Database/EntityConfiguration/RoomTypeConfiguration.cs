using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.HasKey( e => e.Id );

        builder.Property( e => e.Name )
            .IsRequired()
            .HasMaxLength( 200 );

        builder.Property( e => e.DailyPrice )
            .HasColumnType( "decimal(18,2)" );

        builder.Property( e => e.Currency )
            .IsRequired()
            .HasMaxLength( 3 );

        ValueComparer<List<string>> listComparer = new ValueComparer<List<string>>(
            ( c1, c2 ) => c1!.SequenceEqual( c2! ),
            c => c.Aggregate( 0, ( a, v ) => HashCode.Combine( a, v.GetHashCode() ) ),
            c => c.ToList() );

        builder.Property( e => e.Services )
            .HasConversion(
                v => string.Join( ',', v ),
                v => v.Split( ',', StringSplitOptions.RemoveEmptyEntries ).ToList() )
            .Metadata.SetValueComparer( listComparer );

        builder.Property( e => e.Amenities )
            .HasConversion(
                v => string.Join( ',', v ),
                v => v.Split( ',', StringSplitOptions.RemoveEmptyEntries ).ToList() )
            .Metadata.SetValueComparer( listComparer );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( e => e.PropertyId )
            .OnDelete( DeleteBehavior.Cascade );
    }
}