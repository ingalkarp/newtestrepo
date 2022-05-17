namespace ECart.Interfaces
{
    public interface ICartService
    {
        void AddProductToCart(int userId, int itemId);
        void RemoveCartItem(int userId, int itemId);
        void DeleteOneCartItem(int userId, int itemId);
        int GetCartItemCount(int userId);
        void MergeCart(int tempUserId, int permUserId);
        int ClearCart(int userId);
        string GetCartId(int userId);
    }
}
