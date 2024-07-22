using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        public string? PurchaseHistory { get; set; }

        public string? ShippingAddress { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
