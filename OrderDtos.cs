using System.ComponentModel.DataAnnotations;

namespace Assignment3
{
    public class OrderDtos
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderProductDto> OrderProducts { get; set; }
    }

    public class OrderProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}