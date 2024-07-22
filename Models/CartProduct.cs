using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class CartProduct
    {
        [Required]
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
