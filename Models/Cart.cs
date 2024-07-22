using System.ComponentModel.DataAnnotations;
namespace Assignment3.Models
    
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
