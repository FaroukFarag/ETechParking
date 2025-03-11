using ETechParking.Application.Dtos.Locations.Shifts;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Shifts;

public class ConfirmShiftDtoValidator : AbstractValidator<CloseShiftDto>
{
    public ConfirmShiftDtoValidator()
    {
        RuleFor(cs => cs.Id)
            .NotNull();

        RuleFor(cs => cs.EndDateTime)
            .NotNull();

        RuleFor(cs => cs.TotalCash)
            .NotNull();

        RuleFor(cs => cs.TotalCredit)
            .NotNull();
    }
}
