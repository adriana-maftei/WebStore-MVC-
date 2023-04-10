namespace WebStoreMVC.Models
{
    [Table("Genres")]
    public class Genre
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string GenreName { get; set; }

        public List<Game> Games { get; set; }
    }
}