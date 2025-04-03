using Microsoft.EntityFrameworkCore;
using PizzaDinner.Backend.WebApi.Models;
using PizzaDinner.Models;

namespace PizzaDinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)      // Configurar el modelo de datos explícitamente usando 'Fluent API'
        {
            // Configuración de Pizza
            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza");                            // Transcripción de nombre de tabla
                entity.HasKey(p => p.Id);                           // El 'Id' será la PK
                entity.Property(p => p.Id).ValueGeneratedOnAdd();   // Valor de PK autogenerado

                entity.Property(p => p.Name)                // Configuración de Propiedad 'Name'
                    .IsRequired()                                   // Requerido
                    .HasMaxLength(50);                              // Máximos caracteres (50)

                entity.Property(p => p.Description)         // Configuración de Propiedad 'Description'   
                    .HasMaxLength(255);                             // Máximos caracteres (255)

                entity.Property(p => p.Price)               // Configuración de Propiedad 'Price'
                    .HasColumnType("decimal(5, 2)")                 // ?
                    .IsRequired();                                  // Requerido

                entity.Property(p => p.IsVegetarian)        // Configuración de Propiedad 'Description'
                    .HasDefaultValue(false);                        // Valor por defecto (false)

                entity.HasMany(p => p.OrderItems)           // Configuración de relación. Una 'Pizza' puede estar en muchos 'OrderItem'
                    .WithOne(oi => oi.Pizza)                        // Cada 'OrderItem' referencia una 'Pizza'
                    .HasForeignKey(oi => oi.PizzaId)                // FK en 'OrderItem'
                    .OnDelete(DeleteBehavior.Restrict);             // Evitar borrar una 'Pizza' si está en un 'OrderItem'
            });

            // Configuración de Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");                            // Transcripción de nombre de tabla
                entity.HasKey(o => o.Id);                           // El 'Id' será la PK
                entity.Property(o => o.Id).ValueGeneratedOnAdd();   // Valor de PK autogenerado

                entity.Property(o => o.OrderDate)           // Configuración de Propiedad 'OrderDate'
                    .IsRequired()                                   // Requerido
                    .HasDefaultValueSql("GETUTCDATE()");            // Hora actual

                entity.Property(o => o.CustomerName)        // Configuración de Propiedad 'CustomerName'
                    .IsRequired()                                   // Requerido
                    .HasMaxLength(50);                              // Máximos caracteres (50)

                entity.Property(o => o.CustomerEmail)       // Configuración de Propiedad 'CustomerEmail'
                    .IsRequired();                                  // Requerido

                entity.Property(o => o.DeliveryAddress)     // Configuración de Propiedad 'Description'
                    .IsRequired()                                   // Requerido
                    .HasMaxLength(200);                             // Máximos caracteres (200)

                entity.Property(o => o.TotalPrice)          // Configuración de Propiedad 'TotalPrice'
                    .HasColumnType("decimal(5, 2)");                // ?

                entity.Property(o => o.Status)              // Configuración de Propiedad 'Status'
                    .HasDefaultValue(OrderStatus.Pending);          // Valor por defecto (Pending)

                entity.HasMany(o => o.OrderItems)           // Configuración de relación. Un 'Order' tiene muchos 'OrderItems'
                    .WithOne(oi => oi.Order)                        // Cada 'OrderItem' pertenece a un 'Order'
                    .HasForeignKey(oi => oi.OrderId)                // FK en 'OrderItem'
                    .OnDelete(DeleteBehavior.Cascade);              // Borrado en cascada (borrar un 'Order' borra todos sus 'OderItem')
            });

            // Configuración de OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");                        // Transcripción de nombre de tabla
                entity.HasKey(oi => oi.Id);                         // El 'Id' será la PK
                entity.Property(oi => oi.Id).ValueGeneratedOnAdd(); // Valor de PK autogenerado

                entity.Property(oi => oi.Quantity)          // Configuración de Propiedad 'Quantity'
                    .IsRequired()                                   // Requerido
                    .HasDefaultValue(1);                            // Valor por defecto (1)

                entity.Property(oi => oi.PriceAtOrderTime)  // Configuración de Propiedad 'PriceAtOrderTime'
                    .HasColumnType("decimal(5, 2)");                // ?
            });

            // Seed de datos (opcional)
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Margarita", Description = "Clásica pizza con tomate y mozzarella", Price = 8.99m, IsVegetarian = true },
                new Pizza { Id = 2, Name = "Pepperoni", Description = "Pizza con pepperoni y queso fundido", Price = 10.50m, IsVegetarian = false }
            );
        }

    }
}
