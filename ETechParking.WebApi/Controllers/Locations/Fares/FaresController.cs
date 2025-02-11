using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Application.Interfaces.Locations.Fares;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Fares;

[Route("api/[controller]")]
[ApiController]
public class FaresController(IFareService fareService) :
    BaseController<IFareService, Fare, FareDto, int>(fareService)
{
}
