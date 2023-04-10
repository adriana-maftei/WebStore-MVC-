namespace WebStoreMVC.Repositories.Clients.Implementation
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public HomeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetGames(string searchTerm = "", int genreID = 0)
        {
            searchTerm = searchTerm.ToLower();

            IEnumerable<Game> gameSearched = await (from Game in dbContext.Games
                                                    join Genre in dbContext.Genres on Game.GenreID equals Genre.Id
                                                    where string.IsNullOrWhiteSpace(searchTerm) ||
                                                    Game != null && Game.Title.ToLower().Contains(searchTerm) //filter to search
                                                    select new Game
                                                    {
                                                        Id = Game.Id,
                                                        Image = Game.Image,
                                                        Publisher = Game.Publisher,
                                                        Title = Game.Title,
                                                        GenreID = Game.GenreID,
                                                        GenreName = Genre.GenreName,
                                                        UnitPrice = Game.UnitPrice
                                                    }).ToListAsync();

            if (genreID > 0)
                gameSearched = gameSearched.Where(a => a.GenreID == genreID).ToList();

            return gameSearched;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await dbContext.Genres.ToListAsync();
        }
    }
}