namespace WebStoreMVC.Repositories.Clients.Interfaces
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCart(string userID);

        Task<ShoppingCart> GetUserCart();

        Task<int> AddItemToCart(int gameId, int quantity);

        Task<int> RemoveItemFromCart(int gameId);

        Task<int> GetItemCountInCart(string userID = "");

        Task<bool> DoCheckout();
    }
}