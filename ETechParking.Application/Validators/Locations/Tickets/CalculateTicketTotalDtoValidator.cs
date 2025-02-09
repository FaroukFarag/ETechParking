using ETechParking.Application.Dtos.Locations.Tickets;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class CalculateTicketTotalDtoValidator : AbstractValidator<CalculateTicketTotalDto>
{
    public CalculateTicketTotalDtoValidator()
    {
        RuleFor(c => c.PlateNumber)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(c => c.ExitDateTime)
           .NotNull();
    }
}
