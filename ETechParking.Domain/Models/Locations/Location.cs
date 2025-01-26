using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Domain.Models.Locations
{
    public class Location : BaseModel<int>
    {
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}
