using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaDinner.Data;
using PizzaDinner.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PizzaDinner.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de las pizzas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las pizzas disponibles
        /// </summary>
        /// <returns>Lista completa de pizzas del menú</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pizza>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return Ok(pizzas);
        }

        /// <summary>
        /// Obtiene una pizza específica por su ID
        /// </summary>
        /// <param name="id">ID numérico de la pizza</param>
        /// <returns>Datos completos de la pizza solicitada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pizza), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "ID inválido",
                    Detail = "El ID debe ser un número positivo"
                });
            }

            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Pizza no encontrada",
                    Detail = $"No existe pizza con el ID {id}"
                });
            }

            return Ok(pizza);
        }

        /// <summary>
        /// Actualiza los datos de una pizza existente
        /// </summary>
        /// <param name="id">ID de la pizza a modificar</param>
        /// <param name="pizza">Nuevos datos de la pizza</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest(new ValidationProblemDetails(
                    new Dictionary<string, string[]> {
                        { "id", new[] { "El ID del parámetro no coincide con el ID de la pizza" } }
                    }));
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PizzaExists(id))
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "Pizza no encontrada",
                        Detail = ex.Message
                    });
                }

                return Conflict(new ProblemDetails
                {
                    Title = "Conflicto de concurrencia",
                    Detail = "La pizza fue modificada por otro usuario"
                });
            }

            return NoContent();
        }
        
        /// <summary>
        /// Crea una nueva pizza en el menú
        /// </summary>
        /// <param name="pizza">Datos de la nueva pizza</param>
        /// <returns>Datos de la pizza creada</returns>
        /// <remarks>
        /// Ejemplo de request:
        /// 
        /// POST /api/Pizza
        /// {
        ///     "name": "Margarita",
        ///     "description": "Pizza básica
        ///     "price": 10.99
        ///     "isVegetarian": true
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Pizza), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            if (await _context.Pizzas.AnyAsync(p => p.Name == pizza.Name))
            {
                return Conflict(new ProblemDetails
                {
                    Title = "Pizza duplicada",
                    Detail = $"Ya existe una pizza con el nombre {pizza.Name}"
                });
            }

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }

        /// <summary>
        /// Elimina una pizza del menú
        /// </summary>
        /// <param name="id">ID de la pizza a eliminar</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePizza(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "ID inválido",
                    Detail = "El ID debe ser un número positivo"
                });
            }

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Pizza no encontrada",
                    Detail = $"No existe pizza con el ID {id}"
                });
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}