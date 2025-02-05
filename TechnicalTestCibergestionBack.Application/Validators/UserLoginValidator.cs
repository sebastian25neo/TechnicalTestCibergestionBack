using FluentValidation;
using TechnicalTestCibergestionBack.Application.DTOs;

namespace TechnicalTestCibergestionBack.Application.Validators;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El campo Username es requerido")
            .Length(3, 20).WithMessage("El Username debe tener entre 3 y 20 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(5).WithMessage("La contraseña debe tener al menos 5 caracteres");
    }
}
