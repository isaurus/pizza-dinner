using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaDinner.Data;
using PizzaDinner.Models;

namespace PizzaDinner.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de las pizzas
    /// </summary>
    [ApiController]
    [Route("pizzas")]
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
        [Route("")]
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
        /// <param name="idPizza">ID numérico de la pizza</param>
        /// <returns>Datos completos de la pizza solicitada</returns>
        [HttpGet]
        [Route("{idPizza}")]
        [ProducesResponseType(typeof(Pizza), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pizza>> GetPizza(int idPizza)
        {
            
            if (idPizza <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "ID inválido",
                    Detail = "El ID debe ser un número positivo"
                });
            }

            var pizza = await _context.Pizzas.FindAsync(idPizza);

            if (pizza == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Pizza no encontrada",
                    Detail = $"No existe pizza con el ID {idPizza}"
                });
            }

            return Ok(pizza);
        }

        /// <summary>
        /// Actualiza los datos de una pizza existente
        /// </summary>
        /// <param name="idPizza">ID de la pizza a modificar</param>
        /// <param name="pizza">Nuevos datos de la pizza</param>
        [HttpPut]
        [Route("{idPizza}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPizza(int idPizza, Pizza pizza)
        {
            if (idPizza != pizza.Id)
            {
                return BadRequest(new ValidationProblemDetails(
                    new Dictionary<string, string[]> {
                        { "idPizza", new[] { "El ID del parámetro no coincide con el ID de la pizza" } }
                    }));
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PizzaExists(idPizza))
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
        [Route("")]
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
        /// <param name="idPizza">ID único de la pizza a eliminar</param>
        [HttpDelete]
        [Route("{idPizza}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePizza(int idPizza)
        {
            if (idPizza <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "ID inválido",
                    Detail = "El ID debe ser un número positivo"
                });
            }

            var pizza = await _context.Pizzas.FindAsync(idPizza);
            if (pizza == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Pizza no encontrada",
                    Detail = $"No existe pizza con el ID {idPizza}"
                });
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int idPizza)
        {
            return _context.Pizzas.Any(e => e.Id == idPizza);
        }
    }
}