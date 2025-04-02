using Microsoft.EntityFrameworkCore;
using PizzaDinner.Backend.WebApi.Models;
using PizzaDinner.Models;

namespace PizzaDinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)      // Configurar el modelo de datos explícitamente usando 'Fluent API'
        {

            // Define la PK de 'OrderItem'
            modelBuilder.Entity<OrderItem>()
                .HasKey(e => e.Id);

            // Relación 'Order' > 'OrderItem' ('One-To-Many')
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)         // Un 'Order' tiene muchos 'OrderItems'
                .WithOne(o => o.Order)              // Cada 'OrderItem' pertenece a un 'Order'
                .HasForeignKey(o => o.OrderId)      // FK en 'OrderItem'
                .OnDelete(DeleteBehavior.Cascade);  // Borrado en cascada (borrar un 'Order' borra todos sus 'OderItem')

            // Relación 'Pizza' > 'OrderItem' ('One-To-Many')
            modelBuilder.Entity<Pizza>()
                .HasMany(o => o.OrderItems)         // Una 'Pizza' puede estar en muchos 'OrderItem'
                .WithOne(o => o.Pizza)              // Cada 'OrderItem' referencia una 'Pizza'
                .HasForeignKey(o => o.PizzaId)      // FK en 'OrderItem'
                .OnDelete(DeleteBehavior.Restrict); // Evitar borrar una 'Pizza' si está en un 'OrderItem'

            // Transripción de nombre para las tablas
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");

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
