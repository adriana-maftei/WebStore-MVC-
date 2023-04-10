namespace WebStoreMVC.Controllers
{
    [Authorize] //just the logged user can use this controller
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRepository.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> AddItem(int gameId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await cartRepository.AddItemToCart(gameId, quantity);
            if (redirect == 0)
                return Ok(cartCount);

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int gameId)
        {
            var cartCount = await cartRepository.RemoveItemFromCart(gameId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await cartRepository.GetItemCountInCart();
            return View(cartItem);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isChecketOut = await cartRepository.DoCheckout();

            if (!isChecketOut)
                throw new Exception("Something wrong in server side");

            return RedirectToAction("Index", "Home");
        }
    }
}