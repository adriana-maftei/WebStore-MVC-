namespace WebStoreMVC.Repositories.Clients.Interfaces
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Game>> GetGames(string searchTerm = "", int genreID = 0);

        Task<IEnumerable<Genre>> GetGenres();
    }
}