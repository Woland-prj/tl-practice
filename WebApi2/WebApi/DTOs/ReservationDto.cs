namespace WebApi.DTOs;

public record ReservationDto(
    Guid Id,
    Guid PropertyId,
    Guid RoomTypeId,
    DateOnly ArrivalDate,
    DateOnly DepartureDate,
    TimeOnly ArrivalTime,
    TimeOnly DepartureTime,
    string GuestName,
    string GuestPhoneNumber,
    decimal Total,
    string Currency,
    bool IsCanceled );

public record CreateReservationDto(
    Guid PropertyId,
    Guid RoomTypeId,
    DateOnly ArrivalDate,
    DateOnly DepartureDate,
    TimeOnly ArrivalTime,
    TimeOnly DepartureTime,
    string GuestName,
    string GuestPhoneNumber );