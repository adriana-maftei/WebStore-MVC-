namespace WebStoreMVC.Repositories.Admin.Implementation
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext dbContext;

        public GameService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Add(Game model)
        {
            try
            {
                dbContext.Games.Add(model);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Game model)
        {
            try
            {
                dbContext.Games.Update(model);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data != null)
                    return false;

                dbContext.Games.Remove(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Game FindById(int id)
        {
            return dbContext.Games.Find(id);
        }

        public IEnumerable<Game> GetAll()
        {
            var data = (from game in dbContext.Games
                        join genre in dbContext.Genres on game.GenreID equals genre.Id
                        select new Game
                        {
                            Id = game.Id,
                            Title = game.Title,
                            Publisher = game.Publisher,
                            UnitPrice = game.UnitPrice,
                            GenreID = game.GenreID,
                            GenreName = genre.GenreName
                        }).ToList();
            return data;
        }
    }
}