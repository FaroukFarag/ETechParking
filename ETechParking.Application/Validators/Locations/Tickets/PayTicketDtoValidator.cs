using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class PayTicketDtoValidator : AbstractValidator<PayTicketDto>
{
    public PayTicketDtoValidator()
    {
        RuleFor(c => c.PlateNumber)
           .NotEmpty()
           .MaximumLength(50);
    }
}
