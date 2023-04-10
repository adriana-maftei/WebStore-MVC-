namespace WebStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int genreID = 0)
        {
            IEnumerable<Game> games = await _homeRepository.GetGames(searchTerm, genreID);
            IEnumerable<Genre> genres = await _homeRepository.GetGenres();

            GameDisplayModel gameModel = new GameDisplayModel
            {
                Games = games,
                Genres = genres,
                searchTerm = searchTerm,
                GenreID = genreID
            };
            return View(gameModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}