using Domain.Entities;

namespace Domain.Interfaces;

public interface IRoomTypeRepository
{
    IEnumerable<RoomType> GetByPropertyId( Guid propertyId );
    RoomType? GetById( Guid id );
    RoomType Add( RoomType roomType );
    RoomType Update( RoomType roomType );
    void Delete( Guid id );
}