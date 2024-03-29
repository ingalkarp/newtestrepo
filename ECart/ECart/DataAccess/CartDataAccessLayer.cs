﻿using ECart.Interfaces;
using ECart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECart.DataAccess
{

    public class CartDataAccessLayer : ICartService
    {
        readonly ProductDBContext _dbContext;

        public CartDataAccessLayer(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddProductToCart(int userId, int itemId)
        {
            string cartId = GetCartId(userId);
            int quantity = 1;

            CartItems existingCartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == itemId && x.CartId == cartId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _dbContext.Entry(existingCartItem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                CartItems cartItems = new CartItems
                {
                    CartId = cartId,
                    ProductId = itemId,
                    Quantity = quantity
                };
                _dbContext.CartItems.Add(cartItems);
                _dbContext.SaveChanges();
            }
        }

        public string GetCartId(int userId)
        {
            try
            {
                Cart cart = _dbContext.Cart.FirstOrDefault(x => x.UserId == userId);

                if (cart != null)
                {
                    return cart.CartId;
                }
                else
                {
                    return CreateCart(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        string CreateCart(int userId)
        {
            try
            {
                Cart shoppingCart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };

                _dbContext.Cart.Add(shoppingCart);
                _dbContext.SaveChanges();

                return shoppingCart.CartId;
            }
            catch
            {
                throw;
            }
        }

        public void RemoveCartItem(int userId, int itemId)
        {
            try
            {
                string cartId = GetCartId(userId);
                CartItems cartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == itemId && x.CartId == cartId);

                _dbContext.CartItems.Remove(cartItem);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteOneCartItem(int userId, int itemId)
        {
            try
            {
                string cartId = GetCartId(userId);
                CartItems cartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == itemId && x.CartId == cartId);

                cartItem.Quantity -= 1;
                _dbContext.Entry(cartItem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public int GetCartItemCount(int userId)
        {
            string cartId = GetCartId(userId);

            if (!string.IsNullOrEmpty(cartId))
            {
                int cartItemCount = _dbContext.CartItems.Where(x => x.CartId == cartId).Sum(x => x.Quantity);

                return cartItemCount;
            }
            else
            {
                return 0;
            }
        }

        public void MergeCart(int tempUserId, int permUserId)
        {
            try
            {
                if (tempUserId != permUserId && tempUserId > 0 && permUserId > 0)
                {
                    string tempCartId = GetCartId(tempUserId);
                    string permCartId = GetCartId(permUserId);

                    List<CartItems> tempCartItems = _dbContext.CartItems.Where(x => x.CartId == tempCartId).ToList();

                    foreach (CartItems item in tempCartItems)
                    {
                        CartItems cartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == item.ProductId && x.CartId == permCartId);

                        if (cartItem != null)
                        {
                            cartItem.Quantity += item.Quantity;
                            _dbContext.Entry(cartItem).State = EntityState.Modified;
                        }
                        else
                        {
                            CartItems newCartItem = new CartItems
                            {
                                CartId = permCartId,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            };
                            _dbContext.CartItems.Add(newCartItem);
                        }
                        _dbContext.CartItems.Remove(item);
                        _dbContext.SaveChanges();
                    }
                    DeleteCart(tempCartId);
                }
            }
            catch
            {
                throw;
            }
        }

        public int ClearCart(int userId)
        {
            try
            {
                string cartId = GetCartId(userId);
                List<CartItems> cartItem = _dbContext.CartItems.Where(x => x.CartId == cartId).ToList();

                if (!string.IsNullOrEmpty(cartId))
                {
                    foreach (CartItems item in cartItem)
                    {
                        _dbContext.CartItems.Remove(item);
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

        void DeleteCart(string cartId)
        {
            Cart cart = _dbContext.Cart.Find(cartId);
            _dbContext.Cart.Remove(cart);
            _dbContext.SaveChanges();
        }
    }
}
