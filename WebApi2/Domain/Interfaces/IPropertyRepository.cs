using Domain.Entities;

namespace Domain.Interfaces;

public interface IPropertyRepository
{
    IEnumerable<Property> GetAll();
    Property? GetById( Guid id );
    Property Add( Property property );
    Property Update( Property property );
    void Delete( Guid id );
    IEnumerable<Property> GetByCity( string city );

    IEnumerable<(Property Property, RoomType RoomType)> SearchAvailable(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int guests,
        decimal? maxPrice = null );
}