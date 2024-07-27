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
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // getting all cart items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return Ok(await _context.Carts.Include(c => c.CartProducts).ToListAsync());
        }

        // getting single cart item
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                return NotFound(new { message = "Cart not found." });
            }

            return Ok(cart);
        }

        // Creating cart
        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = _mapper.Map<Cart>(cartDto);

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        // updating cart
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _context.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found." });
            }

            cart.UserId = cartDto.UserId;
            cart.CartProducts = cartDto.CartProducts.Select(cp => new CartProduct
            {
                ProductId = cp.ProductId,
                Quantity = cp.Quantity
            }).ToList();

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cart);
        }

        // deleting cart
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found." });
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}