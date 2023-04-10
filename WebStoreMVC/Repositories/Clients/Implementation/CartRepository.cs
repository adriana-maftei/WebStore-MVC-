namespace WebStoreMVC.Repositories.Clients.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userMng;
        private readonly IHttpContextAccessor contextAccessor;

        public CartRepository(ApplicationDbContext dbContext, UserManager<IdentityUser> userMng, IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.userMng = userMng;
            this.contextAccessor = contextAccessor;
        }

        private string GetUserID()
        {
            ClaimsPrincipal user = contextAccessor.HttpContext.User;
            string userID = userMng.GetUserId(user);
            return userID;
        }

        public async Task<ShoppingCart> GetCart(string userID)
        {
            var cart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(a => a.UserID == userID);
            return cart;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            string userID = GetUserID();
            if (userID is null)
                throw new Exception("User not logged in");

            var cart = await dbContext.ShoppingCarts.Include(a => a.CartDetails)
                                                    .ThenInclude(a => a.Game)
                                                    .ThenInclude(a => a.Genre)
                                                    .Where(a => a.UserID == userID).FirstOrDefaultAsync();
            return cart;
        }

        public async Task<int> AddItemToCart(int gameId, int quantity)
        {
            string userID = GetUserID();
            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                //create a cart
                if (string.IsNullOrEmpty(userID))
                    throw new Exception("User not logged in");

                var cart = await GetCart(userID);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserID = userID
                    };
                    dbContext.ShoppingCarts.Add(cart);
                }
                dbContext.SaveChanges();

                //add cart details
                var cartItem = dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCartID == cart.Id && a.GameID == gameId);
                if (cartItem is not null)
                    cartItem.Quantity += quantity;
                else
                {
                    var book = dbContext.Games.Find(gameId);
                    cartItem = new CartDetails
                    {
                        GameID = gameId,
                        ShoppingCartID = cart.Id,
                        Quantity = quantity,
                        UnitPrice = book.UnitPrice
                    };
                    dbContext.CartDetails.Add(cartItem);
                }
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetItemCountInCart(userID);
            return cartItemCount;
        }

        public async Task<int> RemoveItemFromCart(int gameId)
        {
            string userID = GetUserID();

            try
            {
                if (string.IsNullOrEmpty(userID))
                    throw new Exception("User not logged in");

                var cart = await GetCart(userID);
                if (cart is null)
                    throw new Exception("Invalid cart");

                //change cart details
                var cartItem = dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCartID == cart.Id && a.GameID == gameId);
                if (cartItem is null)
                    throw new Exception("Cart is empty");
                else if (cartItem.Quantity == 1)
                    dbContext.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetItemCountInCart(userID);
            return cartItemCount;
        }

        public async Task<int> GetItemCountInCart(string userID = "")
        {
            if (string.IsNullOrEmpty(userID))
                userID = GetUserID();

            var data = await (from cart in dbContext.ShoppingCarts
                              join CartDetail in dbContext.CartDetails
                              on cart.Id equals CartDetail.ShoppingCartID
                              select new { CartDetail.Id }).ToListAsync();
            return data.Count;
        }

        public async Task<bool> DoCheckout()
        {
            var transaction = dbContext.Database.BeginTransaction();
            try
            {
                var userID = GetUserID();
                if (string.IsNullOrEmpty(userID))
                    throw new Exception("User not logged in");

                var cart = await GetCart(userID);
                if (cart is null)
                    throw new Exception("Invalid cart");

                //insert data in orderDetail from cartDetail

                var cartDetail = dbContext.CartDetails
                    .Where(a => a.ShoppingCartID == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");

                var order = new Order
                {
                    UserID = userID,
                    Date = DateTime.Now,
                    OrderStatusID = 1 //complete by default
                };

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetails
                    {
                        GameID = item.GameID,
                        OrderID = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    dbContext.OrderDetails.Add(orderDetail);
                }
                dbContext.SaveChanges();

                //remove data from cartDetail
                dbContext.CartDetails.RemoveRange(cartDetail);
                dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}