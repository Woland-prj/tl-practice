using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryRoomTypeRepository : IRoomTypeRepository
{
    private readonly ConcurrentDictionary<Guid, RoomType> _roomTypes = new();

    public IEnumerable<RoomType> GetByPropertyId( Guid propertyId )
    {
        return _roomTypes.Values
            .Where( rt => rt.PropertyId == propertyId )
            .AsEnumerable();
    }

    public RoomType? GetById( Guid id )
    {
        _roomTypes.TryGetValue( id, out var roomType );
        return roomType;
    }

    public RoomType Add( RoomType roomType )
    {
        _roomTypes.TryAdd( roomType.Id, roomType );
        return roomType;
    }

    public RoomType Update( RoomType roomType )
    {
        _roomTypes[ roomType.Id ] = roomType;
        return roomType;
    }

    public void Delete( Guid id )
    {
        _roomTypes.TryRemove( id, out _ );
    }
}