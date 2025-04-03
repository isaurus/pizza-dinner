using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaDinner.Backend.WebApi.Models;
using PizzaDinner.Data;

namespace PizzaDinner.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // GET BY ID
        public async Task<ActionResult<Order>> GetOrder(int idOrder)
        {
            if (idOrder <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "ID inválido",
                    Detail = "El ID debe ser un número positivo"
                });
            }

            var order = await _context.Orders.FindAsync(idOrder);

            if (order == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Pizza no encontrada",
                    Detail = $"No existe un pedido con el ID {idOrder}"
                });
            }

            return Ok(order);
        }

        // POST
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (await _context.Orders.AnyAsync(o => o.Id == order.Id))
            {
                return Conflict(new ProblemDetails
                {
                    Title = "Pedido duplicado",
                    Detail = $"Ya existe un pedido con el Id {order.Id}"
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetOrders", new {id = order.Id}, order);
        }
        /*
        // PUT
        public Task<IActionResult> PutOrder(int idOrder, Order order)
        {

        }*/
    }
}
