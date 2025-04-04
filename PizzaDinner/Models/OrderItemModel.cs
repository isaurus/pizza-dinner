namespace PizzaDinner.Backend.WebApi.Models
{
    public class OrderItemModel
    {
        /// <summary>
        /// El ID de la pizza (Mayor que 0).
        /// </summary>
        /// <example>33</example>
        public int PizzaId { get; set; }

        /// <summary>
        /// La cantidad de pizzas de un tipo (Mayor que 0).
        /// </summary>
        /// <example>2</example>
        public int Quantity { get; set; } = 1;
    }
}
