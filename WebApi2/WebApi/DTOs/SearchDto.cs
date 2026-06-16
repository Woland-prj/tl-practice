namespace WebApi.DTOs;

public record SearchResultDto(
    PropertyDto Property,
    RoomTypeDto RoomType );

public record SearchRequestDto(
    string City,
    DateOnly ArrivalDate,
    DateOnly DepartureDate,
    int Guests,
    decimal? MaxPrice );