using Domain.Entities;

namespace Domain.Interfaces;

public interface IPropertyService
{
    IEnumerable<Property> GetAllProperties();
    Property? GetPropertyById( Guid id );
    Property CreateProperty( Property property );
    Property UpdateProperty( Property property );
    void DeleteProperty( Guid id );

    IEnumerable<RoomType> GetRoomTypes( Guid propertyId );
    RoomType? GetRoomTypeById( Guid id );
    RoomType CreateRoomType( RoomType roomType );
    RoomType UpdateRoomType( RoomType roomType );
    void DeleteRoomType( Guid id );
}