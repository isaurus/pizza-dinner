namespace PizzaDinner.Backend.WebApi.DTOs
{
    public class OrderItemDto
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
