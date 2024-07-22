using System.ComponentModel.DataAnnotations;
namespace Assignment3.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
