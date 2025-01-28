using ETechParking.Application.Interfaces.Locations;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController(ILocationService locationService) : ControllerBase
    {
        private readonly ILocationService _locationService = locationService;
    }
}
