﻿namespace WebStoreMVC.Repositories.Clients.Implementation
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userMng;
        private readonly IHttpContextAccessor contextAccessor;

        public UserOrderRepository(ApplicationDbContext dbContext, UserManager<IdentityUser> userMng, IHttpContextAccessor contextAccessor)
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

        public async Task<IEnumerable<Order>> GetUserOrders()
        {
            var userID = GetUserID();
            if (userID is null)
                throw new Exception("User not logged in");

            var orders = await dbContext.Orders
                .Include(a => a.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.Game)
                .ThenInclude(a => a.Genre)
                .Where(a => a.UserID == userID)
                .ToListAsync();

            return orders;
        }
    }
}