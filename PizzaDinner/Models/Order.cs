using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaDinner.Backend.WebApi.Models
{
    public class Order
    {
        // Las anotaciones están comentadas porque se ha configurado en 'AppDbContext' (Fluent API)

        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // [Required]
        // [MaxLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        // [Required]
        // [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        // [Required]
        // [MaxLength (200)]
        public string DeliveryAddress { get; set; } = string.Empty;

        // [Column(TypeName = "decimal(5,2)")]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();     // Propiedad de Navegación 'One-To-Many'
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
}
