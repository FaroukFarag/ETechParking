using ETechParking.Application.Dtos.Locations.Users;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Users;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(c => c.UserName)
            .NotEmpty();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
