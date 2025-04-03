namespace PizzaDinner.Backend.WebApi.DTOs
{
    public class OrderItemResponseDto
    {
        public string PizzaName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }
    }
}
