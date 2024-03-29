﻿using ECart.Interfaces;
using ECart.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECart.DataAccess
{
    public class WishlistDataAccessLayer : IWishlistService
    {
        readonly ProductDBContext _dbContext;

        public WishlistDataAccessLayer(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ToggleWishlistItem(int userId, int itemId)
        {
            string wishlistId = GetWishlistId(userId);
            WishlistItems existingWishlistItem = _dbContext.WishlistItems.FirstOrDefault(x => x.ProductId == itemId && x.WishlistId == wishlistId);

            if (existingWishlistItem != null)
            {
                _dbContext.WishlistItems.Remove(existingWishlistItem);
                _dbContext.SaveChanges();
            }
            else
            {
                WishlistItems wishlistItem = new WishlistItems
                {
                    WishlistId = wishlistId,
                    ProductId = itemId,
                };
                _dbContext.WishlistItems.Add(wishlistItem);
                _dbContext.SaveChanges();
            }
        }

        public int ClearWishlist(int userId)
        {
            try
            {
                string wishlistId = GetWishlistId(userId);
                List<WishlistItems> wishlistItem = _dbContext.WishlistItems.Where(x => x.WishlistId == wishlistId).ToList();

                if (!string.IsNullOrEmpty(wishlistId))
                {
                    foreach (WishlistItems item in wishlistItem)
                    {
                        _dbContext.WishlistItems.Remove(item);
                        _dbContext.SaveChanges();
                    }
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public string GetWishlistId(int userId)
        {
            try
            {
                Wishlist wishlist = _dbContext.Wishlist.FirstOrDefault(x => x.UserId == userId);

                if (wishlist != null)
                {
                    return wishlist.WishlistId;
                }
                else
                {
                    return CreateWishlist(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        string CreateWishlist(int userId)
        {
            try
            {
                Wishlist wishList = new Wishlist
                {
                    WishlistId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };

                _dbContext.Wishlist.Add(wishList);
                _dbContext.SaveChanges();

                return wishList.WishlistId;
            }
            catch
            {
                throw;
            }
        }
    }
}
