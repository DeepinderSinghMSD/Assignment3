namespace Assignment3.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Pricing { get; set; }
        public decimal ShippingCost { get; set; }
        public double Rating { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

