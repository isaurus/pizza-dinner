namespace PizzaDinner.Backend.WebApi.Models
{
    public class PizzaResponseModel
    {
        /// <summary>
        /// ID único de la pizza.
        /// </summary>
        /// <example>5</example>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la pizza (Máx. 50 caracteres).
        /// </summary>
        /// <example>Cabramelizada</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la pizza (Máx. 255 caracteres).
        /// </summary>
        /// <example>Más dulce que nunca</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Precio de la pizza (Mayor que 0)
        /// </summary>
        /// <example>10.99</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Si es o no vegetariana
        /// </summary>
        public bool IsVegetarian { get; set; }
    }
}
