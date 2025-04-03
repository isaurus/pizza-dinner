using FluentValidation;
using PizzaDinner.Backend.WebApi.DTOs;

namespace PizzaDinner.Backend.WebApi.Validations
{
    using FluentValidation;

    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(o => o.CustomerName)
                .NotEmpty().WithMessage("El nombre del cliente es obligatorio")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres");

            RuleFor(o => o.CustomerEmail)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio")
                .EmailAddress().WithMessage("Formato de correo inválido")
                .MaximumLength(100);

            RuleFor(o => o.DeliveryAddress)
                .NotEmpty().WithMessage("La dirección es obligatoria")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres");

            RuleFor(o => o.Items)
                .NotEmpty().WithMessage("El pedido debe contener al menos un artículo")
                .Must(items => items.All(i => i.Quantity > 0))
                .WithMessage("La cantidad debe ser mayor a cero");
        }
    }
}
