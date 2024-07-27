using System.ComponentModel.DataAnnotations;

namespace Assignment3
{
    public class CartDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<CartProductDto> CartProducts { get; set; } = new List<CartProductDto>();
    }

    public class CartProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}