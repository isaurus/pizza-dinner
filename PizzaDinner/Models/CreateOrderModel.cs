namespace PizzaDinner.Backend.WebApi.Models
{
    public class CreateOrderModel
    {
        /// <summary>
        /// Nombre de usuario (Máx. 50 caracteres).
        /// </summary>
        /// <example>John Doe</example>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Email de usuario (Máx. 100 caracteres)
        /// </summary>
        /// <example>johndoe@example.com</example>
        public string CustomerEmail { get; set; } = string.Empty;

        /// <summary>
        /// Dirección de envío (Máx. 255 caracteres)
        /// </summary>
        /// <example>Calle Triana, 5</example>
        public string DeliveryAddress { get; set; } = string.Empty;

        /// <summary>
        /// Lista de artículos del pedido (mín. 1 elemento).
        /// </summary>
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}
