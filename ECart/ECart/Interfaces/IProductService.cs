using ECart.Dto;
using ECart.Models;
using System.Collections.Generic;

namespace ECart.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        int AddProduct(Product product);
        int UpdateProduct(Product product);
        Product GetProductData(int itemId);
        string DeleteProduct(int itemId);
        List<Categories> GetCategories();
        List<Product> GetSimilarProducts(int itemId);
        List<CartItemDto> GetProductsAvailableInCart(string cartId);
        List<Product> GetProductsAvailableInWishlist(string wishlistID);
    }
}
