using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }

        public string? Images { get; set; }

        [StringLength(1000, ErrorMessage = "Text cannot exceed 1000 characters.")]
        public string Text { get; set; }
    }
}
