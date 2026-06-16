using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryReservationRepository : IReservationRepository
{
    private readonly ConcurrentDictionary<Guid, Reservation> _reservations = new();

    public IEnumerable<Reservation> GetAll(
        Guid? propertyId = null,
        Guid? roomTypeId = null,
        DateOnly? arrivalDate = null,
        DateOnly? departureDate = null,
        string? guestName = null )
    {
        var query = _reservations.Values.AsEnumerable();

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
                r.GuestName.Contains( guestName, StringComparison.OrdinalIgnoreCase ) );
        }

        return query;
    }

    public Reservation? GetById( Guid id )
    {
        _reservations.TryGetValue( id, out var reservation );
        return reservation;
    }

    public Reservation Add( Reservation reservation )
    {
        _reservations.TryAdd( reservation.Id, reservation );
        return reservation;
    }

    public void Update( Reservation reservation )
    {
        _reservations[ reservation.Id ] = reservation;
    }

    public bool IsRoomTypeAvailable( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        var reservations = GetByRoomTypeId( roomTypeId );

        return !reservations.Any( r =>
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate );
    }

    public IEnumerable<Reservation> GetByRoomTypeId( Guid roomTypeId )
    {
        return _reservations.Values
            .Where( r => r.RoomTypeId == roomTypeId )
            .AsEnumerable();
    }
}