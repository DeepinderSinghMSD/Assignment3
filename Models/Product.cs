using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid image URL.")]
        public string Image { get; set; } 

        [Range(0.01, double.MaxValue, ErrorMessage = "Pricing must be greater than zero.")]
        public decimal Pricing { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Shipping cost cannot be negative.")]
        public decimal ShippingCost { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }

        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<CartProduct>? CartProducts { get; set; } = new List<CartProduct>();
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
    }
}

