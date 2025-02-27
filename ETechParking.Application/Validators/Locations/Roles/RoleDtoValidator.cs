using ETechParking.Application.Dtos.Locations.Roles;
using FluentValidation;

namespace ETechParking.Application.Validators.Locations.Roles;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}

