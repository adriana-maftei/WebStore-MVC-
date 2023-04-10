namespace WebStoreMVC.Models
{
    [Table("Order Status")]
    public class OrderStatus
    {
        public int Id { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required, MaxLength(20)]
        public string? StatusName { get; set; }
    }
}