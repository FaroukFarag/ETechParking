using ETechParking.Application.Dtos.Locations.Fares;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Fares;

public class FareDtoValidator : AbstractValidator<FareDto>
{
    public FareDtoValidator()
    {
        RuleFor(c => c.Amount)
            .NotNull();

        RuleFor(c => c.FareType)
            .NotNull();

        RuleFor(c => c.EnterGracePeriod)
            .NotNull();

        RuleFor(c => c.ExitGracePeriod)
            .NotNull();

        RuleFor(c => c.LocationId)
            .NotNull();
    }
}
