namespace WebStoreMVC.Models
{
    [Table("Cart Details")]
    public class CartDetails
    {
        public int Id { get; set; }

        [Required]
        public int ShoppingCartID { get; set; }

        [Required]
        public int GameID { get; set; }

        public ShoppingCart ShopppingCart { get; set; }
        public Game Game { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}