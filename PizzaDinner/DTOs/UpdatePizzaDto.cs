namespace PizzaDinner.Backend.WebApi.DTOs
{
    public class UpdatePizzaDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
