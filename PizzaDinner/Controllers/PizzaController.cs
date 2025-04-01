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

        // Inyección de dependencias en constructor
        public PizzaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las pizzas
        /// </summary>
        /// <returns>La lista de pizzas disponibles</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pizza>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas.ToListAsync();   // Almacena las pistas en una lista usando '_context' (polimorfismo)
            return Ok(pizzas);
        }

        /// <summary>
        /// Obtiene una pizza por su ID
        /// </summary>
        /// <param name="id">ID de la pizza</param>
        /// <returns>La pizza solicitada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pizza))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        /// <summary>
        /// Actualiza una pizza existente
        /// </summary>
        /// <param name="id">ID de la pizza a actualizar</param>
        /// <param name="pizza">Datos actualizados de la pizza</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Crea una nueva pizza
        /// </summary>
        /// <param name="pizza">La pizza a añadir</param>
        /// <returns>La pizza creada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Pizza))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);     // Añade la pizza pasada por parámetro a la tabla de la DB a través de '_context'
            await _context.SaveChangesAsync();  // Guarda los datos

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);   // Proporciona al cliente la URL del recurso creado
        }

        /// <summary>
        /// Elimina una pizza
        /// </summary>
        /// <param name="id">El ID de la pizza a eliminar</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
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
