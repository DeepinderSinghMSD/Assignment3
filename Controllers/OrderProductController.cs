using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _context.OrderProducts.Include(op => op.Order).Include(op => op.Product).ToListAsync();
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(int orderId, int productId)
        {
            var orderProduct = await _context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == productId);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderProduct), new { orderId = orderProduct.OrderId, productId = orderProduct.ProductId }, orderProduct);
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> PutOrderProduct(int orderId, int productId, OrderProduct orderProduct)
        {
            if (orderId != orderProduct.OrderId || productId != orderProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(orderId, productId))
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

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderProduct(int orderId, int productId)
        {
            var orderProduct = await _context.OrderProducts
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == productId);

            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductExists(int orderId, int productId)
        {
            return _context.OrderProducts.Any(op => op.OrderId == orderId && op.ProductId == productId);
        }
    }
}
