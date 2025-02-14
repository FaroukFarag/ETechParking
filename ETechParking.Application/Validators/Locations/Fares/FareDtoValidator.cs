using ETechParking.Application.Dtos.Locations.Fares;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Fares;

public class FareDtoValidator : AbstractValidator<FareDto>
{
    public FareDtoValidator()
    {
        RuleFor(f => f.Amount)
            .NotNull();

        RuleFor(f => f.FareType)
            .NotNull();

        RuleFor(f => f.EnterGracePeriod)
            .NotNull();

        RuleFor(f => f.ExitGracePeriod)
            .NotNull();

        RuleFor(f => f.LocationId)
            .NotNull();
    }
}
