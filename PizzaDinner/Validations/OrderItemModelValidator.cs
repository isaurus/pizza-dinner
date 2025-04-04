namespace PizzaDinner.Backend.WebApi.Validations
{
    using FluentValidation;
    using PizzaDinner.Backend.WebApi.Models;

    public class OrderItemModelValidator : AbstractValidator<OrderItemModel>
    {
        public OrderItemModelValidator()
        {
            RuleFor(i => i.PizzaId)
                .GreaterThan(0).WithMessage("El ID de la pizza es inválido");

            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero");
        }
    }
}
