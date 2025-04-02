using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PizzaDinner.Models;

namespace PizzaDinner.Backend.WebApi.Models
{
    public class OrderItem   // Tabla intermedia por la relación 'Many-To-Many entre 'Order' y 'Pizza'
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }   // Foreign Key de 'Order'

        [ForeignKey(nameof(OrderId))]       // Para definir explícitamente la relación la propiedad de navegación 'Order' y su FK 'OrderId'
        public Order Order { get; set; }    // Propiedad de Navegación 'One-To-One'

        [Required]
        public Guid PizzaId { get; set; }   // Foreign Key de 'Pizza'

        [ForeignKey(nameof(PizzaId))]
        public Pizza Pizza { get; set; }    // Propiedad de Navegación  'One-To-One'

        [Required]
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PriceAtOrderTime { get; set; }
    }
}
