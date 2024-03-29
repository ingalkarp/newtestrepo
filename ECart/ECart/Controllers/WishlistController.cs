﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ECart.Interfaces;
using ECart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECart.Controllers
{
    [Route("api/[controller]")]
    public class WishlistController : Controller
    {
        readonly IWishlistService _wishlistService;
        readonly IProductService _productService;
        readonly IUserService _userService;

        public WishlistController(IWishlistService wishlistService, IProductService productService, IUserService userService)
        {
            _wishlistService = wishlistService;
            _productService = productService;
            _userService = userService;
        }

            [HttpGet("{userId}")]
        public async Task<List<Product>> Get(int userId)
        {
            return await Task.FromResult(GetUserWishlist(userId)).ConfigureAwait(true);
        }

       [Authorize]
        [HttpPost]
        [Route("ToggleWishlist/{userId}/{itemId}")]
        public async Task<List<Product>> Post(int userId, int itemId)
        {
            _wishlistService.ToggleWishlistItem(userId, itemId);
            return await Task.FromResult(GetUserWishlist(userId)).ConfigureAwait(true);
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public int Delete(int userId)
        {
            return _wishlistService.ClearWishlist(userId);
        }

        List<Product> GetUserWishlist(int userId)
        {
            bool user = _userService.isUserExists(userId);
            if (user)
            {
                string Wishlistid = _wishlistService.GetWishlistId(userId);
                return _productService.GetProductsAvailableInWishlist(Wishlistid);
            }
            else
            {
                return new List<Product>();
            }

        }
    }
}
