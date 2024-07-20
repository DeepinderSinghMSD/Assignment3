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
    public class CartProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProduct>>> GetCartProducts()
        {
            return await _context.CartProducts.Include(cp => cp.Cart).Include(cp => cp.Product).ToListAsync();
        }

        [HttpGet("{cartId}/{productId}")]
        public async Task<ActionResult<CartProduct>> GetCartProduct(int cartId, int productId)
        {
            var cartProduct = await _context.CartProducts
                .Include(cp => cp.Cart)
                .Include(cp => cp.Product)
                .FirstOrDefaultAsync(cp => cp.CartId == cartId && cp.ProductId == productId);

            if (cartProduct == null)
            {
                return NotFound();
            }

            return cartProduct;
        }

        [HttpPost]
        public async Task<ActionResult<CartProduct>> PostCartProduct(CartProduct cartProduct)
        {
            _context.CartProducts.Add(cartProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartProduct), new { cartId = cartProduct.CartId, productId = cartProduct.ProductId }, cartProduct);
        }

        [HttpPut("{cartId}/{productId}")]
        public async Task<IActionResult> PutCartProduct(int cartId, int productId, CartProduct cartProduct)
        {
            if (cartId != cartProduct.CartId || productId != cartProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(cartProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartProductExists(cartId, productId))
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

        [HttpDelete("{cartId}/{productId}")]
        public async Task<IActionResult> DeleteCartProduct(int cartId, int productId)
        {
            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(cp => cp.CartId == cartId && cp.ProductId == productId);

            if (cartProduct == null)
            {
                return NotFound();
            }

            _context.CartProducts.Remove(cartProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartProductExists(int cartId, int productId)
        {
            return _context.CartProducts.Any(cp => cp.CartId == cartId && cp.ProductId == productId);
        }
    }
}
