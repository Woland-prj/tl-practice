namespace WebApi.DTOs;

public record RoomTypeDto(
    Guid Id,
    Guid PropertyId,
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount,
    List<string> Services,
    List<string> Amenities );

public record CreateRoomTypeDto(
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount,
    List<string>? Services,
    List<string>? Amenities );

public record UpdateRoomTypeDto(
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount,
    List<string>? Services,
    List<string>? Amenities );