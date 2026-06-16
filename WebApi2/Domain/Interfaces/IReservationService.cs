using Domain.Entities;

namespace Domain.Interfaces;

public interface IReservationService
{
    IEnumerable<(Property Property, RoomType RoomType)> Search(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int guests,
        decimal? maxPrice = null);

    Reservation CreateReservation(
        Guid propertyId,
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber);

    IEnumerable<Reservation> GetAllReservations(
        Guid? propertyId = null,
        DateOnly? arrivalDate = null,
        DateOnly? departureDate = null,
        string? guestName = null);

    Reservation? GetReservationById(Guid id);
    void CancelReservation(Guid id);
}
