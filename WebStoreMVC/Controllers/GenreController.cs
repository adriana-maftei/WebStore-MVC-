namespace WebStoreMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = genreService.Add(model);
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
            var record = genreService.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = genreService.Update(model);
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
            var result = genreService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id)
        {
            var data = genreService.GetAll();
            return View(data);
        }
    }
}