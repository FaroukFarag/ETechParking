using ETechParking.Application.Dtos.Locations.Tickets;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class TicketDtoValidator : AbstractValidator<TicketDto>
{
    public TicketDtoValidator()
    {
        RuleFor(t => t.PlateNumber)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(t => t.EntryDateTime)
            .NotNull();

        RuleFor(t => t.ExitDateTime)
            .NotNull();

        RuleFor(t => t.LocationId)
            .NotNull();

        RuleFor(t => t.ShiftId)
            .NotNull();
    }
}
