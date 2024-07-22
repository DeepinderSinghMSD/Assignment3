using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class OrderProduct
    {

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }


    }
}
