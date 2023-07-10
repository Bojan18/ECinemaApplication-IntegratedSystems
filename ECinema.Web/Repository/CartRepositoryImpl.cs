using ECinema.Web.Data;
using ECinema.Web.Models.DomainModels;
using ECinema.Web.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Web.Repository
{
    public class CartRepositoryImpl : CartRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ECinemaApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public CartRepositoryImpl(ApplicationDbContext db, UserManager<ECinemaApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }
        private string GetUserId()
        {
            var user = contextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var counter = await db.ShoppingCarts
                .Where(cart => cart.UserId == userId)
                .SelectMany(cart => cart.CartDetails)
                .CountAsync();

            return counter;
        }


        public async Task<int> AddItem(int ticketId, int quantity)
        {
            string userId = GetUserId();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Invalid User");
                }

                var cart = GetCart(userId);

                if (cart is null)
                {
                    var newCart = new ShoppingCart { UserId = userId };
                    db.ShoppingCarts.Add(newCart);
                    await db.SaveChangesAsync(); // Use await for async SaveChangesAsync()
                }

                var cartItem = db.CartDetails.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.TicketId == ticketId);
                if (cartItem is null)
                {
                    cartItem = new CartDetail { TicketId = ticketId, ShoppingCartId = cart.Id, Quantity = quantity };
                    db.CartDetails.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }

                await db.SaveChangesAsync(); // Use await for async SaveChangesAsync()
                await transaction.CommitAsync(); // Use await for async CommitAsync()
            }
            catch (Exception ex)
            {
                // Handle any exceptions
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }


        async Task<int> CartRepository.DeleteItem(int ticketId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is null or empty.");
                }

                var cart = GetCart(userId);

                if (cart is null)
                {
                    // user has to have a cart to remove the item.
                    throw new Exception("Cart is null.");
                }

                var cartItem = db.CartDetails.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.TicketId == ticketId);

                if (cartItem is null)
                {
                    throw new Exception("Cart Item is null.");
                }
                else if (cartItem.Quantity == 1)
                {
                    db.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity -= 1;
                }
                db.SaveChanges();

            }
            catch (Exception ex) { }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                throw new Exception("Invalid user.");
            }

            var cart = await db.ShoppingCarts
                .Include(x => x.CartDetails)
                .ThenInclude(cd => cd.Ticket)
                .FirstOrDefaultAsync();

            return cart;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = db.ShoppingCarts.FirstOrDefault(x => x.UserId == userId);

            return cart;
        }
    }
}