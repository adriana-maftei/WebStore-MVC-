namespace WebStoreMVC.Repositories.Admin.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext dbContext;

        public GenreService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Add(Genre model)
        {
            try
            {
                dbContext.Genres.Add(model);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Genre model)
        {
            try
            {
                dbContext.Genres.Update(model);
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

                dbContext.Genres.Remove(data);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre FindById(int id)
        {
            return dbContext.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return dbContext.Genres.ToList();
        }
    }
}