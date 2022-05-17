﻿using System.Collections.Generic;
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

        /// <summary>
        /// Get the shopping cart for a user upon Login. If the user logs in for the first time, creates the shopping cart.
        /// </summary>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns>The count of items in shopping cart</returns>
        [Authorize]
        [HttpGet]
        [Route("SetShoppingCart/{oldUserId}/{newUserId}")]
        public int Get(int oldUserId, int newUserId)
        {
            _cartService.MergeCart(oldUserId, newUserId);
            return _cartService.GetCartItemCount(newUserId);
        }

        /// <summary>
        /// Get the list of items in the shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<List<CartItemDto>> Get(int userId)
        {
            string cartid = _cartService.GetCartId(userId);
            return await Task.FromResult(_productService.GetProductsAvailableInCart(cartid)).ConfigureAwait(true);
        }

        /// <summary>
        /// Add a single item into the shopping cart. If the item already exists, increase the quantity by one
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns>Count of items in the shopping cart</returns>
        [HttpPost]
        [Route("AddToCart/{userId}/{itemId}")]
        public int Post(int userId, int itemId)
        {
            _cartService.AddProductToCart(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Reduces the quantity by one for an item in shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPut("{userId}/{itemId}")]
        public int Put(int userId, int itemId)
        {
            _cartService.DeleteOneCartItem(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Delete a single item from the cart 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{itemId}")]
        public int Delete(int userId, int itemId)
        {
            _cartService.RemoveCartItem(userId, itemId);
            return _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Clear the shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public int Delete(int userId)
        {
            return _cartService.ClearCart(userId);
        }
    }
}
