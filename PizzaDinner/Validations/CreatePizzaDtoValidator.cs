using FluentValidation;
using PizzaDinner.Backend.WebApi.DTOs;

namespace PizzaDinner.Backend.WebApi.Validations
{
    public class CreatePizzaDtoValidator : AbstractValidator<CreatePizzaDto>
    {
        public CreatePizzaDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de la pizza es obligatorio")
                .MaximumLength(50).WithMessage("El nombre no puede exceder 50 caracteres");

            RuleFor(p => p.Description)
                .MaximumLength(255).WithMessage("La descripción no puede exceder 255 caracteres");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0")
                .ScalePrecision(2, 5).WithMessage("Formato inválido: máximo 5 dígitos con 2 decimales");
        }
    }
}
