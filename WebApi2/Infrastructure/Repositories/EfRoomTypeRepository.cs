using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfRoomTypeRepository : IRoomTypeRepository
{
    private readonly Data.AppDbContext _context;

    public EfRoomTypeRepository( Data.AppDbContext context )
    {
        _context = context;
    }

    public IEnumerable<RoomType> GetByPropertyId( Guid propertyId )
    {
        return _context.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .ToList();
    }

    public RoomType? GetById( Guid id )
    {
        return _context.RoomTypes.Find( id );
    }

    public RoomType Add( RoomType roomType )
    {
        _context.RoomTypes.Add( roomType );
        _context.SaveChanges();
        return roomType;
    }

    public RoomType Update( RoomType roomType )
    {
        RoomType? tracked = _context.RoomTypes.Find( roomType.Id );
        if ( tracked is not null )
        {
            _context.Entry( tracked ).CurrentValues.SetValues( roomType );
        }
        _context.SaveChanges();
        return roomType;
    }

    public void Delete( Guid id )
    {
        RoomType? roomType = _context.RoomTypes.Find( id );
        if ( roomType is null )
        {
            return;
        }

        _context.RoomTypes.Remove( roomType );
        _context.SaveChanges();
    }
}