using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;
using AutoMapper;

namespace Assignment3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDtos>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDtos>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDtos>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { message = "order not found." });
            }

            return Ok(_mapper.Map<OrderDtos>(order));
        }


        [HttpPost]
        public async Task<ActionResult<OrderDtos>> CreateOrder([FromBody] OrderDtos orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if UserId exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == orderDto.UserId);
            if (!userExists)
            {
                return BadRequest(new { Error = "Invalid UserId" });
            }

            // Check if all ProductIds exist
            var invalidProducts = orderDto.OrderProducts.Where(op => !_context.Products.Any(p => p.Id == op.ProductId)).ToList();
            if (invalidProducts.Any())
            {
                return BadRequest(new { Error = "Invalid ProductId(s)", Products = invalidProducts });
            }

            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, _mapper.Map<OrderDtos>(order));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDtos orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest(new { message = "Order ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == orderDto.UserId);
            if (!userExists)
            {
                return BadRequest(new { Error = "Invalid UserId" });
            }

            var invalidProducts = orderDto.OrderProducts.Where(op => !_context.Products.Any(p => p.Id == op.ProductId)).ToList();
            if (invalidProducts.Any())
            {
                return BadRequest(new { Error = "Invalid ProductId(s)", Products = invalidProducts });
            }

            _mapper.Map(orderDto, order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound(new { message = "Order not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "order not found." });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
