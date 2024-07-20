namespace Assignment3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Rating { get; set; }
        public string Images { get; set; }
        public string Text { get; set; }
    }
}
