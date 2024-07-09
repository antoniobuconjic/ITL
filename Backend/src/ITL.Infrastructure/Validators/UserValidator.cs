using FluentValidation;
using ITL.Domain.DTOs;

namespace ITL.Infrastructure.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Email).NotNull();
        RuleFor(x => x.Password).NotNull();
        RuleFor(x => x.Role).NotNull();
    }
}
