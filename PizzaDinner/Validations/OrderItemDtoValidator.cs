namespace PizzaDinner.Backend.WebApi.Validations
{
    using FluentValidation;
    using PizzaDinner.Backend.WebApi.DTOs;

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(i => i.PizzaId)
                .GreaterThan(0).WithMessage("El ID de la pizza es inválido");

            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero");
        }
    }
}
