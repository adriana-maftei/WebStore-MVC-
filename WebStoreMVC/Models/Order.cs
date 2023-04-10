namespace WebStoreMVC.Models
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int OrderStatusID { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool isDeleted { get; set; } = false;
        public List<OrderDetails> OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}