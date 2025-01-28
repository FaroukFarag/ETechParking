using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations;

public class LocationDto : BaseModelDto<int>
{
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
}
