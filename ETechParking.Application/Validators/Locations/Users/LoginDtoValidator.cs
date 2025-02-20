using ETechParking.Application.Dtos.Locations.Users;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Users;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty();

        RuleFor(l => l.Password)
            .NotEmpty();

        RuleFor(l => l.StartDateTime)
            .NotEmpty();
    }
}
