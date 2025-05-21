using ETechParking.Application.Dtos.Locations.Tickets;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class CalculateTicketTotalDtoValidator : AbstractValidator<CalculateTicketTotalDto>
{
    public CalculateTicketTotalDtoValidator()
    {
        RuleFor(ctt => ctt.TicketNumber)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(ctt => ctt.ExitDateTime)
           .NotNull();
    }
}
