namespace PizzaDinner.Backend.WebApi.Models

{
    public class OrderItemResponseModel
    {
        /// <summary>
        /// Nombre de la pizza (Máx. 50 caracteres).
        /// </summary>
        /// <example>Cabramelizada</example>
        public string PizzaName { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de una misma pizza (Mayor que 0).
        /// </summary>
        /// <example>2</example>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio de la pizza en el momento del pedido
        /// </summary>
        public decimal PriceAtOrderTime { get; set; }
    }
}
