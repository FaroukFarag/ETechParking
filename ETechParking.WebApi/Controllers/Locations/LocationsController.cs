using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Interfaces.Locations;
using ETechParking.Domain.Models.Locations;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations;

[Route("api/[controller]")]
[ApiController]
public class LocationsController(ILocationService locationService) :
    BaseController<ILocationService, Location, LocationDto, int>(locationService)
{
}
