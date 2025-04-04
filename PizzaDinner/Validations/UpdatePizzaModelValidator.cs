using FluentValidation;
using PizzaDinner.Backend.WebApi.Models;

namespace PizzaDinner.Backend.WebApi.Validations
{
    public class UpdatePizzaModelValidator : AbstractValidator<UpdatePizzaModel>
    {
        public UpdatePizzaModelValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("El nombre de la Pizza no puede estar vacío")
                .MaximumLength(50).WithMessage("El nombre no puede ocupar más de 50 caracteres");

            RuleFor(u => u.Description)
                .NotEmpty().WithMessage("La Pizza debe contener una descripción")
                .MaximumLength(255).WithMessage("La descripción no puede ocupar más de 255 caracteres");

            RuleFor(u => u.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0")
                .ScalePrecision(2, 5).WithMessage("Formato inválido: máximo 5 dígitos con 2 decimales");
        }
    }
}
