namespace WebStoreMVC.Models.DTOs
{
    public class GameDisplayModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string searchTerm { get; set; } = "";
        public int GenreID { get; set; } = 0;
    }
}