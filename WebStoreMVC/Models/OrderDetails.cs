namespace WebStoreMVC.Models
{
    [Table("Order Details")]
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public Order Order { get; set; }
        public Game Game { get; set; }
    }
}