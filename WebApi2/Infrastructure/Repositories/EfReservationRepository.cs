using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly Data.AppDbContext _context;

    public EfReservationRepository( Data.AppDbContext context )
    {
        _context = context;
    }

    public IEnumerable<Reservation> GetAll(
        Guid? propertyId = null,
        Guid? roomTypeId = null,
        DateOnly? arrivalDate = null,
        DateOnly? departureDate = null,
        string? guestName = null )
    {
        IQueryable<Reservation> query = _context.Reservations.AsQueryable();

        if ( propertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == propertyId.Value );
        }

        if ( roomTypeId.HasValue )
        {
            query = query.Where( r => r.RoomTypeId == roomTypeId.Value );
        }

        if ( arrivalDate.HasValue )
        {
            query = query.Where( r => r.ArrivalDate == arrivalDate.Value );
        }

        if ( departureDate.HasValue )
        {
            query = query.Where( r => r.DepartureDate == departureDate.Value );
        }

        if ( !string.IsNullOrWhiteSpace( guestName ) )
        {
            query = query.Where( r =>
                EF.Functions.ILike( r.GuestName, $"%{guestName}%" ) );
        }

        return query.ToList();
    }

    public Reservation? GetById( Guid id )
    {
        return _context.Reservations.Find( id );
    }

    public Reservation Add( Reservation reservation )
    {
        _context.Reservations.Add( reservation );
        _context.SaveChanges();
        return reservation;
    }

    public void Update( Reservation reservation )
    {
        _context.Reservations.Update( reservation );
        _context.SaveChanges();
    }

    public bool IsRoomTypeAvailable( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        return !_context.Reservations.Any( r =>
            r.RoomTypeId == roomTypeId &&
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate );
    }

    public IEnumerable<Reservation> GetByRoomTypeId( Guid roomTypeId )
    {
        return _context.Reservations
            .Where( r => r.RoomTypeId == roomTypeId )
            .ToList();
    }
}