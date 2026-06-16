namespace WebApi.DTOs;

public record PropertyDto(
    Guid Id,
    string Name,
    string Country,
    string City,
    string Address,
    decimal Latitude,
    decimal Longitude );

public record CreatePropertyDto(
    string Name,
    string Country,
    string City,
    string Address,
    decimal Latitude,
    decimal Longitude );

public record UpdatePropertyDto(
    string Name,
    string Country,
    string City,
    string Address,
    decimal Latitude,
    decimal Longitude );