using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

public class ReservationService : IReservationService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public IEnumerable<(Property Property, RoomType RoomType)> Search(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int guests,
        decimal? maxPrice = null )
    {
        if ( string.IsNullOrWhiteSpace( city ) )
        {
            throw new ArgumentException( "City is required for search." );
        }

        if ( arrivalDate >= departureDate )
        {
            throw new ArgumentException( "Departure date must be after arrival date." );
        }

        if ( guests <= 0 )
        {
            throw new ArgumentException( "Guest count must be greater than zero." );
        }

        return _propertyRepository.SearchAvailable(
            city, arrivalDate, departureDate, guests, maxPrice );
    }

    public Reservation CreateReservation(
        Guid propertyId,
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new ArgumentException( "Guest name is required." );
        }

        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new ArgumentException( "Guest phone number is required." );
        }

        if ( arrivalDate >= departureDate )
        {
            throw new ArgumentException( "Departure date must be after arrival date." );
        }

        if ( arrivalDate <= DateOnly.FromDateTime( DateTime.UtcNow ) )
        {
            throw new ArgumentException( "Arrival date must be in the future." );
        }

        var property = _propertyRepository.GetById( propertyId );

        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with ID {propertyId} not found." );
        }

        var roomType = _roomTypeRepository.GetById( roomTypeId );

        if ( roomType is null )
        {
            throw new KeyNotFoundException( $"RoomType with ID {roomTypeId} not found." );
        }

        if ( roomType.PropertyId != propertyId )
        {
            throw new InvalidOperationException( "RoomType does not belong to the specified Property." );
        }

        var isAvailable = _reservationRepository.IsRoomTypeAvailable(
            roomTypeId, arrivalDate, departureDate );

        if ( !isAvailable )
        {
            throw new InvalidOperationException( "Room type is not available for the selected dates." );
        }

        var nights = departureDate.DayNumber - arrivalDate.DayNumber;
        var total = roomType.DailyPrice * nights;

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            RoomTypeId = roomTypeId,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            ArrivalTime = arrivalTime,
            DepartureTime = departureTime,
            GuestName = guestName,
            GuestPhoneNumber = guestPhoneNumber,
            Total = total,
            Currency = roomType.Currency,
            IsCanceled = false
        };

        return _reservationRepository.Add( reservation );
    }

    public IEnumerable<Reservation> GetAllReservations(
        Guid? propertyId = null,
        DateOnly? arrivalDate = null,
        DateOnly? departureDate = null,
        string? guestName = null )
    {
        return _reservationRepository.GetAll(
            propertyId, null, arrivalDate, departureDate, guestName );
    }

    public Reservation? GetReservationById( Guid id )
    {
        return _reservationRepository.GetById( id );
    }

    public void CancelReservation( Guid id )
    {
        var reservation = _reservationRepository.GetById( id );

        if ( reservation is null )
        {
            throw new KeyNotFoundException( $"Reservation with ID {id} not found." );
        }

        if ( reservation.IsCanceled )
        {
            throw new InvalidOperationException( "Reservation is already canceled." );
        }

        reservation.IsCanceled = true;
        _reservationRepository.Update( reservation );
    }
}