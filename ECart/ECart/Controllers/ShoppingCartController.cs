using System.Collections.Generic;
using System.Threading.Tasks;
using ECart.Dto;
using ECart.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECart.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        readonly ICartService _cartService;
        readonly IProductService _productService;

        public ShoppingCartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [Route("SetShoppingCart/{oldUserId}/{newUserId}")]
        public int Get(int oldUserId, int newUserId)
        {
            _cartService.MergeCart(oldUserId, newUserId);
            return _cartService.GetCartItemCount(newUserId);
        }
  [HttpGet("{userId}")]
        public async Task<List<CartItemDto>> Get(int userId)
        {
            string cartid = _cartService.GetCartId(userId);
            return await Task.FromResult(_productService.GetProductsAvailableInCart(cartid)).ConfigureAwait(true);
        }

         [HttpPost]
        [Route("AddToCart/{userId}/{itemId}")]
        public int Post(int userId, int itemId)
        {
            _cartService.AddProductToCart(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

       [HttpPut("{userId}/{itemId}")]
        public int Put(int userId, int itemId)
        {
            _cartService.DeleteOneCartItem(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

           [HttpDelete("{userId}/{itemId}")]
        public int Delete(int userId, int itemId)
        {
            _cartService.RemoveCartItem(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

         [HttpDelete("{userId}")]
        public int Delete(int userId)
        {
            return _cartService.ClearCart(userId);
        }
    }
}
