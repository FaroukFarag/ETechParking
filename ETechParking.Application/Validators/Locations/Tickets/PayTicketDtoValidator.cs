using ETechParking.Application.Dtos.Locations.Tickets;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Tickets;

public class PayTicketDtoValidator : AbstractValidator<PayTicketDto>
{
    public PayTicketDtoValidator()
    {
        RuleFor(pt => pt.PlateNumber)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(pt => pt.TransactionType)
            .NotNull();
    }
}
