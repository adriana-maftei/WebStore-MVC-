namespace WebStoreMVC.Controllers
{
    [Authorize] //just the logged user can use this controller
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository userOrderRepository;

        public UserOrderController(IUserOrderRepository userOrderRepository)
        {
            this.userOrderRepository = userOrderRepository;
        }

        public async Task<IActionResult> UserOrders()
        {
            var orders = await userOrderRepository.GetUserOrders();
            return View(orders);
        }
    }
}