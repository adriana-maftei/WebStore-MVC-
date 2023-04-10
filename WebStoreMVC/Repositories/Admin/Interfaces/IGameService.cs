namespace WebStoreMVC.Repositories.Admin.Interfaces
{
    public interface IGameService
    {
        bool Add(Game model);

        bool Update(Game model);

        bool Delete(int id);

        Game FindById(int id);

        IEnumerable<Game> GetAll();
    }
}