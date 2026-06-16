using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfPropertyRepository : IPropertyRepository
{
    private readonly Data.AppDbContext _context;

    public EfPropertyRepository( Data.AppDbContext context )
    {
        _context = context;
    }

    public IEnumerable<Property> GetAll()
    {
        return _context.Properties.ToList();
    }

    public Property? GetById( Guid id )
    {
        return _context.Properties.Find( id );
    }

    public Property Add( Property property )
    {
        _context.Properties.Add( property );
        _context.SaveChanges();
        return property;
    }

    public Property Update( Property property )
    {
        Property? tracked = _context.Properties.Find( property.Id );
        if ( tracked is not null )
        {
            _context.Entry( tracked ).CurrentValues.SetValues( property );
        }
        _context.SaveChanges();
        return property;
    }

    public void Delete( Guid id )
    {
        Property? property = _context.Properties.Find( id );
        if ( property is null )
        {
            return;
        }

        _context.Properties.Remove( property );
        _context.SaveChanges();
    }

    public IEnumerable<Property> GetByCity( string city )
    {
        return _context.Properties
            .Where( p => EF.Functions.ILike( p.City, city ) )
            .ToList();
    }

    public IEnumerable<(Property Property, RoomType RoomType)> SearchAvailable(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int guests,
        decimal? maxPrice = null )
    {
        var query = from p in _context.Properties
            join rt in _context.RoomTypes on p.Id equals rt.PropertyId
            where EF.Functions.ILike( p.City, city )
                  && guests >= rt.MinPersonCount
                  && guests <= rt.MaxPersonCount
                  && ( !maxPrice.HasValue || rt.DailyPrice <= maxPrice.Value )
                  && !_context.Reservations.Any( r =>
                      r.RoomTypeId == rt.Id
                      && !r.IsCanceled
                      && r.ArrivalDate < departureDate
                      && r.DepartureDate > arrivalDate )
            select new
            {
                Property = p,
                RoomType = rt
            };

        return query
            .AsEnumerable()
            .Select( x => ( x.Property, x.RoomType ) )
            .ToList();
    }
}