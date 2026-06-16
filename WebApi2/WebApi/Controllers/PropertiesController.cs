using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PropertyDto>> GetAll()
    {
        IEnumerable<Property> properties = _propertyService.GetAllProperties();

        IEnumerable<PropertyDto> dtos = properties.Select( p => new PropertyDto(
            p.Id, p.Name, p.Country, p.City, p.Address, p.Latitude, p.Longitude ) );

        return Ok( dtos );
    }

    [HttpGet( "{id:guid}" )]
    public ActionResult<PropertyDto> GetById( Guid id )
    {
        Property? property = _propertyService.GetPropertyById( id );

        if ( property is null )
        {
            return NotFound();
        }

        PropertyDto dto = new PropertyDto(
            property.Id, property.Name, property.Country,
            property.City, property.Address, property.Latitude, property.Longitude );

        return Ok( dto );
    }

    [HttpPost]
    public ActionResult<PropertyDto> Create( [FromBody] CreatePropertyDto createDto )
    {
        Property property = new Property
        {
            Name = createDto.Name,
            Country = createDto.Country,
            City = createDto.City,
            Address = createDto.Address,
            Latitude = createDto.Latitude,
            Longitude = createDto.Longitude
        };

        Property created = _propertyService.CreateProperty( property );

        PropertyDto dto = new PropertyDto(
            created.Id, created.Name, created.Country,
            created.City, created.Address, created.Latitude, created.Longitude );

        return CreatedAtAction( nameof( GetById ), new
        {
            id = created.Id
        }, dto );
    }

    [HttpPut( "{id:guid}" )]
    public ActionResult<PropertyDto> Update( Guid id, [FromBody] UpdatePropertyDto updateDto )
    {
        Property property = new Property
        {
            Id = id,
            Name = updateDto.Name,
            Country = updateDto.Country,
            City = updateDto.City,
            Address = updateDto.Address,
            Latitude = updateDto.Latitude,
            Longitude = updateDto.Longitude
        };

        Property updated = _propertyService.UpdateProperty( property );

        PropertyDto dto = new PropertyDto(
            updated.Id,
            updated.Name,
            updated.Country,
            updated.City,
            updated.Address,
            updated.Latitude,
            updated.Longitude );

        return Ok( dto );
    }

    [HttpDelete( "{id:guid}" )]
    public IActionResult Delete( Guid id )
    {
        _propertyService.DeleteProperty( id );
        return NoContent();
    }
}