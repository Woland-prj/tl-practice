using Domain.Entities;

namespace Domain.Interfaces;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll(
        Guid? propertyId = null,
        Guid? roomTypeId = null,
        DateOnly? arrivalDate = null,
        DateOnly? departureDate = null,
        string? guestName = null );

    Reservation? GetById( Guid id );
    Reservation Add( Reservation reservation );
    void Update( Reservation reservation );
    bool IsRoomTypeAvailable( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate );
    IEnumerable<Reservation> GetByRoomTypeId( Guid roomTypeId );
}