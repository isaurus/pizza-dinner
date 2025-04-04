namespace PizzaDinner.Backend.WebApi.Models
{
    public class CreatePizzaModel
    {
        /// <summary>
        /// Nombre de la pizza (Máx. 50 caracteres).
        /// </summary>
        /// <example>Carbonara</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la pizza (Máx. 255 caracteres).
        /// </summary>
        /// <example>Deliciosa pizza cubierta de queso fundido</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Precio de la pizza (Mayor que 0).
        /// </summary>
        /// <example>9.99</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Tipo de pizza (vegetariana o no vegetariana)
        /// </summary>
        public bool IsVegetarian { get; set; }
    }
}
