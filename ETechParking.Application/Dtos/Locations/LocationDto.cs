using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations;

public class LocationDto : BaseModelDto<int>
{
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
}
