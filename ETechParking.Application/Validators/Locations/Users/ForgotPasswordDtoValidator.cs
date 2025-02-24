using ETechParking.Application.Dtos.Locations.Users;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Users;

public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordDtoValidator()
    {
        RuleFor(fp => fp.UserName)
            .NotEmpty();

        RuleFor(fp => fp.NewPassword)
            .NotEmpty();
    }
}
