using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaDinner.Data;

namespace PizzaDinner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Contenedor de DI para registar el 'DbContext'
            builder.Services.AddDbContext<AppDbContextOld>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))    // Cadena de conexión en 'appsettings.json'
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pizza API",
                    Description = "Simple ASP.NET Core Web Api para trabajar con pizzas",
                    Contact = new OpenApiContact
                    {
                        Name = "Isaac Martín",
                        Email = "isaacmartin.dev@gmail.com",
                        Url = new Uri("https://x.com/isaurus_"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                });

                // Configuración para XML Comments
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                else
                {
                    // Opcional: Loggear advertencia si el archivo no existe
                    Console.WriteLine($"XML documentation file not found: {xmlPath}");
                }
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
