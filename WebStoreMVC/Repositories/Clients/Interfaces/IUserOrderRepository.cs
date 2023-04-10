namespace WebStoreMVC.Repositories.Clients.Interfaces
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders();
    }
}