using FluentValidation;
using TechnicalTestCibergestionBack.Application.DTOs;

namespace TechnicalTestCibergestionBack.Application.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El campo Username es requerido")
                .Length(3, 20).WithMessage("El Username debe tener entre 3 y 20 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es requerido.")
                .Must(BeValidRole).WithMessage("El rol debe ser 'Admin' o 'Voter'.");
        }

        private bool BeValidRole(string role)
        {
            return role == "Admin" || role == "Voter";
        }
    }
}
