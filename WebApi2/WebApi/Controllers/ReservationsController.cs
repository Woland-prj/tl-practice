using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpGet( "~/api/search" )]
    public ActionResult<IEnumerable<SearchResultDto>> Search(
        [FromQuery] string city,
        [FromQuery] DateOnly arrivalDate,
        [FromQuery] DateOnly departureDate,
        [FromQuery] int guests,
        [FromQuery] decimal? maxPrice = null )
    {
        var results = _reservationService.Search(
            city, arrivalDate, departureDate, guests, maxPrice );

        IEnumerable<SearchResultDto> dtos = results.Select( r =>
        {
            PropertyDto propertyDto = new PropertyDto(
                r.Property.Id, r.Property.Name, r.Property.Country,
                r.Property.City, r.Property.Address, r.Property.Latitude, r.Property.Longitude );

            RoomTypeDto roomTypeDto = new RoomTypeDto(
                r.RoomType.Id, r.RoomType.PropertyId, r.RoomType.Name,
                r.RoomType.DailyPrice, r.RoomType.Currency,
                r.RoomType.MinPersonCount, r.RoomType.MaxPersonCount,
                r.RoomType.Services, r.RoomType.Amenities );

            return new SearchResultDto( propertyDto, roomTypeDto );
        } );

        return Ok( dtos );
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create( [FromBody] CreateReservationDto createDto )
    {
        Reservation reservation = _reservationService.CreateReservation(
            createDto.PropertyId,
            createDto.RoomTypeId,
            createDto.ArrivalDate,
            createDto.DepartureDate,
            createDto.ArrivalTime,
            createDto.DepartureTime,
            createDto.GuestName,
            createDto.GuestPhoneNumber );

        ReservationDto dto = new ReservationDto(
            reservation.Id, reservation.PropertyId, reservation.RoomTypeId,
            reservation.ArrivalDate, reservation.DepartureDate,
            reservation.ArrivalTime, reservation.DepartureTime,
            reservation.GuestName, reservation.GuestPhoneNumber,
            reservation.Total, reservation.Currency, reservation.IsCanceled );

        return CreatedAtAction( nameof( GetById ), new
        {
            id = reservation.Id
        }, dto );
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll(
        [FromQuery] Guid? propertyId = null,
        [FromQuery] DateOnly? arrivalDate = null,
        [FromQuery] DateOnly? departureDate = null,
        [FromQuery] string? guestName = null )
    {
        IEnumerable<Reservation> reservations = _reservationService.GetAllReservations(
            propertyId, arrivalDate, departureDate, guestName );

        IEnumerable<ReservationDto> dtos = reservations.Select( r => new ReservationDto(
            r.Id, r.PropertyId, r.RoomTypeId,
            r.ArrivalDate, r.DepartureDate,
            r.ArrivalTime, r.DepartureTime,
            r.GuestName, r.GuestPhoneNumber,
            r.Total, r.Currency, r.IsCanceled ) );

        return Ok( dtos );
    }

    [HttpGet( "{id:guid}" )]
    public ActionResult<ReservationDto> GetById( Guid id )
    {
        Reservation? reservation = _reservationService.GetReservationById( id );

        if ( reservation is null )
        {
            return NotFound();
        }

        ReservationDto dto = new ReservationDto(
            reservation.Id, reservation.PropertyId, reservation.RoomTypeId,
            reservation.ArrivalDate, reservation.DepartureDate,
            reservation.ArrivalTime, reservation.DepartureTime,
            reservation.GuestName, reservation.GuestPhoneNumber,
            reservation.Total, reservation.Currency, reservation.IsCanceled );

        return Ok( dto );
    }

    [HttpDelete( "{id:guid}" )]
    public IActionResult Cancel( Guid id )
    {
        _reservationService.CancelReservation( id );
        return NoContent();
    }
}