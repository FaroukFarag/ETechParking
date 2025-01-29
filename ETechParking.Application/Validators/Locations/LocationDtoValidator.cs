using ETechParking.Application.Dtos.Locations;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations;

public class LocationDtoValidator : AbstractValidator<LocationDto>
{
    public LocationDtoValidator()
    {
        RuleFor(c => c.Country)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.City)
            .NotEmpty()
            .MaximumLength(50);
    }
}