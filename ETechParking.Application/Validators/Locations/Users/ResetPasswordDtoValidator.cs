using ETechParking.Application.Dtos.Locations.Users;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Users;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(rp => rp.UserName)
            .NotEmpty();

        RuleFor(rp => rp.OldPassword)
            .NotEmpty();

        RuleFor(rp => rp.NewPassword)
            .NotEmpty();
    }
}
