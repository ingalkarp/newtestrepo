namespace ECart.Interfaces
{
    public interface IWishlistService
    {
        void ToggleWishlistItem(int userId, int itemId);
        int ClearWishlist(int userId);
        string GetWishlistId(int userId);
    }
}
