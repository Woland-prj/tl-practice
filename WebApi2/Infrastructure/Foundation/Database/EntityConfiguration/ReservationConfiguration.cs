using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.HasKey( e => e.Id );

        builder.Property( e => e.GuestName )
            .IsRequired()
            .HasMaxLength( 200 );

        builder.Property( e => e.GuestPhoneNumber )
            .IsRequired()
            .HasMaxLength( 30 );

        builder.Property( e => e.Total )
            .HasColumnType( "decimal(18,2)" );

        builder.Property( e => e.Currency )
            .IsRequired()
            .HasMaxLength( 3 );

        builder.Property( e => e.ArrivalTime )
            .HasColumnType( "time" );

        builder.Property( e => e.DepartureTime )
            .HasColumnType( "time" );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( e => e.PropertyId )
            .OnDelete( DeleteBehavior.Cascade );

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey( e => e.RoomTypeId )
            .OnDelete( DeleteBehavior.Cascade );
    }
}