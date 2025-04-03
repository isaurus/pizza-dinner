﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PizzaDinner.Backend.WebApi.Models;

namespace PizzaDinner.Models
{
    public class Pizza
    {

        // Las anotaciones están comentadas porque se ha configurado en 'AppDbContext' (Fluent API)


        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [Required]
        // [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        // [Required]
        // [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public bool IsVegetarian { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();     // Propiedad de Navegación 'One-To-Many'
    }
}
