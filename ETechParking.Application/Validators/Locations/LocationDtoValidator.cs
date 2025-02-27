using ETechParking.Application.Dtos.Locations;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations;

public class LocationDtoValidator : AbstractValidator<LocationDto>
{
    public LocationDtoValidator()
    {
        RuleFor(l => l.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(l => l.Country)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(l => l.City)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(l => l.Longitude)
            .NotNull();

        RuleFor(l => l.Latitude)
            .NotNull();
    }
}