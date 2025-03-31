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
                    Id = 1,
                    Name = "Margarita",
                    Description = "Clásica pizza con tomate y mozzarella",
                    Price = 8.99m,
                    IsVegetarian = true
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Pepperoni",
                    Description = "Pizza con pepperoni y queso fundido",
                    Price = 10.50m,
                    IsVegetarian = false
                }
            );
        }
    }
}
