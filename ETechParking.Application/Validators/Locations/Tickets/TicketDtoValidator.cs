using ETechParking.Application.Dtos.Locations.Tickets;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class TicketDtoValidator : AbstractValidator<TicketDto>
{
    public TicketDtoValidator()
    {
        RuleFor(c => c.PlateNumber)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(c => c.EntryDateTime)
            .NotNull();

        RuleFor(c => c.ExitDateTime)
            .NotNull();

        RuleFor(c => c.LocationId)
            .NotNull();
    }
}
