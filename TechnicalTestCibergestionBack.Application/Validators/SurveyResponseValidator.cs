using FluentValidation;
using TechnicalTestCibergestionBack.Application.DTOs;

namespace TechnicalTestCibergestionBack.Application.Validators
{
    public class SurveyResponseValidator : AbstractValidator<SurveyResponseDto>
    {
        public SurveyResponseValidator()
        {
            RuleFor(x => x.Score)
                .NotNull().WithMessage("El campo Score es requerido.") // Evita valores nulos
                .InclusiveBetween(0, 10).WithMessage("El Score debe estar entre 0 y 10.");
        }
    }
}
