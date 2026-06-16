using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties/{propertyId:guid}/[controller]" )]
public class RoomTypesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public RoomTypesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RoomTypeDto>> GetByProperty( Guid propertyId )
    {
        IEnumerable<RoomType> roomTypes = _propertyService.GetRoomTypes( propertyId );

        IEnumerable<RoomTypeDto> dtos = roomTypes.Select( rt => new RoomTypeDto(
            rt.Id, rt.PropertyId, rt.Name, rt.DailyPrice,
            rt.Currency, rt.MinPersonCount, rt.MaxPersonCount,
            rt.Services, rt.Amenities ) );

        return Ok( dtos );
    }

    [HttpGet( "~/api/roomtypes/{id:guid}" )]
    public ActionResult<RoomTypeDto> GetById( Guid id )
    {
        RoomType? roomType = _propertyService.GetRoomTypeById( id );

        if ( roomType is null )
        {
            return NotFound();
        }

        RoomTypeDto dto = new RoomTypeDto(
            roomType.Id, roomType.PropertyId, roomType.Name, roomType.DailyPrice,
            roomType.Currency, roomType.MinPersonCount, roomType.MaxPersonCount,
            roomType.Services, roomType.Amenities );

        return Ok( dto );
    }

    [HttpPost]
    public ActionResult<RoomTypeDto> Create( Guid propertyId, [FromBody] CreateRoomTypeDto createDto )
    {
        RoomType roomType = new RoomType
        {
            PropertyId = propertyId,
            Name = createDto.Name,
            DailyPrice = createDto.DailyPrice,
            Currency = createDto.Currency,
            MinPersonCount = createDto.MinPersonCount,
            MaxPersonCount = createDto.MaxPersonCount,
            Services = createDto.Services ?? [ ],
            Amenities = createDto.Amenities ?? [ ]
        };

        RoomType created = _propertyService.CreateRoomType( roomType );

        RoomTypeDto dto = new RoomTypeDto(
            created.Id, created.PropertyId, created.Name, created.DailyPrice,
            created.Currency, created.MinPersonCount, created.MaxPersonCount,
            created.Services, created.Amenities );

        return CreatedAtAction( nameof( GetById ), new
        {
            id = created.Id
        }, dto );
    }

    [HttpPut( "~/api/roomtypes/{id:guid}" )]
    public ActionResult<RoomTypeDto> Update( Guid id, [FromBody] UpdateRoomTypeDto updateDto )
    {
        RoomType roomType = new RoomType
        {
            Id = id,
            PropertyId = Guid.Empty,
            Name = updateDto.Name,
            DailyPrice = updateDto.DailyPrice,
            Currency = updateDto.Currency,
            MinPersonCount = updateDto.MinPersonCount,
            MaxPersonCount = updateDto.MaxPersonCount,
            Services = updateDto.Services ?? [ ],
            Amenities = updateDto.Amenities ?? [ ]
        };

        RoomType updated = _propertyService.UpdateRoomType( roomType );

        RoomTypeDto dto = new RoomTypeDto(
            updated.Id, updated.PropertyId, updated.Name, updated.DailyPrice,
            updated.Currency, updated.MinPersonCount, updated.MaxPersonCount,
            updated.Services, updated.Amenities );

        return Ok( dto );
    }

    [HttpDelete( "~/api/roomtypes/{id:guid}" )]
    public IActionResult Delete( Guid id )
    {
        _propertyService.DeleteRoomType( id );
        return NoContent();
    }
}