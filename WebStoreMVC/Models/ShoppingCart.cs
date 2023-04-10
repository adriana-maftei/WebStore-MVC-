namespace WebStoreMVC.Models
{
    [Table("Shopping Cart")]
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        public bool isDeleted { get; set; } = false;
        public ICollection<CartDetails> CartDetails { get; set; }
    }
}