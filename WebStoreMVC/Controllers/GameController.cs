using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStoreMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IGenreService genreService;

        public GameController(IGameService gameService, IGenreService genreService)
        {
            this.gameService = gameService;
            this.genreService = genreService;
        }

        public IActionResult Add()
        {
            var model = new Game();
            model.GenreList = genreService.GetAll().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Game model)
        {
            model.GenreList = genreService.GetAll().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString(), Selected = g.Id == model.GenreID }).ToList();

            if (!ModelState.IsValid) return View(model);

            var result = gameService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added successfully!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured!";
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var model = gameService.FindById(id);
            model.GenreList = genreService.GetAll().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString(), Selected = g.Id == model.GenreID }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Game model)
        {
            model.GenreList = genreService.GetAll().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString(), Selected = g.Id == model.GenreID }).ToList();

            if (!ModelState.IsValid) return View(model);

            var result = gameService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated successfully!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured!";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = gameService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id)
        {
            var data = gameService.GetAll();
            return View(data);
        }
    }
}