using ETechParking.Application.Dtos.Locations.Users;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Users;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(c => c.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.Email)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(15)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,15}$");

        RuleFor(c => c.PhoneNumber)
            .MaximumLength(15);
    }
}
