using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryPropertyRepository : IPropertyRepository
{
    private readonly ConcurrentDictionary<Guid, Property> _properties = new();
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public InMemoryPropertyRepository( IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public IEnumerable<Property> GetAll()
    {
        return _properties.Values.AsEnumerable();
    }

    public Property? GetById( Guid id )
    {
        _properties.TryGetValue( id, out var property );
        return property;
    }

    public Property Add( Property property )
    {
        _properties.TryAdd( property.Id, property );
        return property;
    }

    public Property Update( Property property )
    {
        _properties[ property.Id ] = property;
        return property;
    }

    public void Delete( Guid id )
    {
        _properties.TryRemove( id, out _ );
    }

    public IEnumerable<Property> GetByCity( string city )
    {
        return _properties.Values
            .Where( p => p.City.Equals( city, StringComparison.OrdinalIgnoreCase ) )
            .AsEnumerable();
    }

    public IEnumerable<(Property Property, RoomType RoomType)> SearchAvailable(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int guests,
        decimal? maxPrice = null )
    {
        var result = new List<(Property, RoomType)>();

        foreach ( var property in GetByCity( city ) )
        {
            var roomTypes = _roomTypeRepository.GetByPropertyId( property.Id );

            foreach ( var roomType in roomTypes )
            {
                if ( guests < roomType.MinPersonCount || guests > roomType.MaxPersonCount )
                {
                    continue;
                }

                if ( maxPrice.HasValue && roomType.DailyPrice > maxPrice.Value )
                {
                    continue;
                }

                var isAvailable = _reservationRepository.IsRoomTypeAvailable(
                    roomType.Id, arrivalDate, departureDate );

                if ( isAvailable )
                {
                    result.Add( ( property, roomType ) );
                }
            }
        }

        return result;
    }
}