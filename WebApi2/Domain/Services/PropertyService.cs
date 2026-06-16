using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public PropertyService( IPropertyRepository propertyRepository, IRoomTypeRepository roomTypeRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    public IEnumerable<Property> GetAllProperties()
    {
        return _propertyRepository.GetAll();
    }

    public Property? GetPropertyById( Guid id )
    {
        return _propertyRepository.GetById( id );
    }

    public Property CreateProperty( Property property )
    {
        if ( string.IsNullOrWhiteSpace( property.Name ) )
        {
            throw new ArgumentException( "Property name is required." );
        }

        if ( string.IsNullOrWhiteSpace( property.City ) )
        {
            throw new ArgumentException( "City is required." );
        }

        property.Id = Guid.NewGuid();
        return _propertyRepository.Add( property );
    }

    public Property UpdateProperty( Property property )
    {
        var existing = _propertyRepository.GetById( property.Id );

        if ( existing is null )
        {
            throw new KeyNotFoundException( $"Property with ID {property.Id} not found." );
        }

        return _propertyRepository.Update( property );
    }

    public void DeleteProperty( Guid id )
    {
        var existing = _propertyRepository.GetById( id );

        if ( existing is null )
        {
            throw new KeyNotFoundException( $"Property with ID {id} not found." );
        }

        _propertyRepository.Delete( id );
    }

    public IEnumerable<RoomType> GetRoomTypes( Guid propertyId )
    {
        return _roomTypeRepository.GetByPropertyId( propertyId );
    }

    public RoomType? GetRoomTypeById( Guid id )
    {
        return _roomTypeRepository.GetById( id );
    }

    public RoomType CreateRoomType( RoomType roomType )
    {
        if ( string.IsNullOrWhiteSpace( roomType.Name ) )
        {
            throw new ArgumentException( "Room type name is required." );
        }

        if ( roomType.DailyPrice <= 0 )
        {
            throw new ArgumentException( "Daily price must be greater than zero." );
        }

        if ( roomType.MinPersonCount <= 0 )
        {
            throw new ArgumentException( "Min person count must be greater than zero." );
        }

        if ( roomType.MaxPersonCount < roomType.MinPersonCount )
        {
            throw new ArgumentException( "Max person count must be greater or equal to min person count." );
        }

        var property = _propertyRepository.GetById( roomType.PropertyId );

        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with ID {roomType.PropertyId} not found." );
        }

        roomType.Id = Guid.NewGuid();
        return _roomTypeRepository.Add( roomType );
    }

    public RoomType UpdateRoomType( RoomType roomType )
    {
        var existing = _roomTypeRepository.GetById( roomType.Id );

        if ( existing is null )
        {
            throw new KeyNotFoundException( $"RoomType with ID {roomType.Id} not found." );
        }

        return _roomTypeRepository.Update( roomType );
    }

    public void DeleteRoomType( Guid id )
    {
        var existing = _roomTypeRepository.GetById( id );

        if ( existing is null )
        {
            throw new KeyNotFoundException( $"RoomType with ID {id} not found." );
        }

        _roomTypeRepository.Delete( id );
    }
}