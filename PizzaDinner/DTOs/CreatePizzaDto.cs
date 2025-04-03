namespace PizzaDinner.Backend.WebApi.DTOs
{
    public class CreatePizzaDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsVegetarian { get; set; }
    }
}
