namespace PizzaDinner.Backend.WebApi.Models
{
    public class UpdatePizzaModel
    {
        /// <summary>
        /// Nuevo nombre de la pizza (Máx. 50 caracteres).
        /// </summary>
        /// <example>Cabramelizada</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Nueva descripción de la pizza (Máx. 255 caracteres).
        /// </summary>
        /// <example>Dulce como nunca antes</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Nuevo precio de la pizza (Mayor que 0).
        /// </summary>
        /// <example>11.99</example>
        public decimal Price { get; set; }
    }
}
