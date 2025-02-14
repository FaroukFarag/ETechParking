using ETechParking.Application.Dtos.Locations.Shifts;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Shifts;

public class ShiftDtoValidator : AbstractValidator<ShiftDto>
{
    public ShiftDtoValidator()
    {
        RuleFor(s => s.StartDateTime)
            .NotNull();

        RuleFor(s => s.LocationId)
            .NotNull();

        RuleFor(s => s.UserId)
            .NotNull();
    }
}
