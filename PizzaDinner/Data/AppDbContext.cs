using Microsoft.EntityFrameworkCore;
using PizzaDinner.Models;

namespace PizzaDinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Transripción de nombre para la tabla ('Pizzas' > 'Pizza')
            modelBuilder.Entity<Pizza>().ToTable("Pizza");

            // Seed inicial de datos (opcional)
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza
                {
                    Id = Guid.Parse("a18be9c0-aa65-4af8-bd17-00bd9344e575"), // GUID fijo
                    Name = "Margarita",
                    Description = "Clásica pizza con tomate y mozzarella",
                    Price = 8.99m,
                    IsVegetarian = true
                },
                new Pizza
                {
                    Id = Guid.Parse("c12d4abb-3e6d-457a-9b9e-9e7aaf4c6c7c"), // Otro GUID fijo
                    Name = "Pepperoni",
                    Description = "Pizza con pepperoni y queso fundido",
                    Price = 10.50m,
                    IsVegetarian = false
                }
            );
        }
    }
}
