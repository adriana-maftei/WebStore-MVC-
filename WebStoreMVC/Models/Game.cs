using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStoreMVC.Models
{
    [Table("Games")]
    public class Game
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string Title { get; set; }

        [Required, MaxLength(40)]
        public string Publisher { get; set; }

        public string? Image { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreID { get; set; }

        [NotMapped] //not in DB
        public string GenreName { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        public List<CartDetails> CartDetails { get; set; }

        [NotMapped]
        public List<SelectListItem>? GenreList { get; set; }
    }
}