namespace PizzaDinner.Backend.WebApi.Models
{
    public class OrderResponseModel
    {
        /// <summary>
        /// ID único del pedido (Mayor que 0).
        /// </summary>
        /// <example>33</example>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en la que se realiza el pedido.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Nombre del cliente (Máx. 50 caracteres).
        /// </summary>
        /// <example>John Doe</example>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Dirección de envío del pedido (Máx. 255 caracteres).
        /// </summary>
        /// <example>Calle Triana, 5</example>
        public string DeliveryAddress { get; set; } = string.Empty;

        /// <summary>
        /// Precio total del pedido (Mayor que 0).
        /// </summary>
        /// <example>23.95</example>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Estado del pedido.
        /// </summary>
        /// <example>Pending</example>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Ítems en el pedido
        /// </summary>
        public List<OrderItemResponseModel> Items { get; set; } = new List<OrderItemResponseModel>();
    }
}
