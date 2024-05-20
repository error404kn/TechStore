using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TechStore.Models
{
    public class Cart
    {
        private readonly TechStoreContext _context;

        public Cart(TechStoreContext context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<TechStoreContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }
        public CartItem GetCartItem(Technics technics) 
        {
            return _context.CartItems.SingleOrDefault(ci => ci.Technics.Id == technics.Id && ci.CartId == Id);
        }
        public void AddToCart(Technics technics, int quantity)
        {
            var cartItem = GetCartItem(technics);

            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    Technics = technics,
                    Quantity = quantity,
                    CartId = Id
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        /*Reduce Quantity*/

        public int ReduceQuantity(Technics technics)
        {
            var cartItem = GetCartItem(technics);
            var remainingQuantity = 0;

            if(cartItem != null)
            {
                if(cartItem.Quantity > 1) 
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
            return remainingQuantity;
        }


        /*Increase Quantity*/
        public int IncreaseQuantity(Technics technics)
        {
            var cartItem = GetCartItem(technics);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    remainingQuantity = ++cartItem.Quantity;
                }
            }
            _context.SaveChanges();
            return remainingQuantity;
        }

        /*You can remove item*/
        public void RemoveFromCart(Technics technics)
        {
            var cartItem = GetCartItem(technics);

            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        /*You can clear cart*/
        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ?? (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                .Include(ci => ci.Technics).ToList());
        }

        public int GetCartTotal()
        {
            return _context.CartItems.Where(ci => ci.CartId == Id).Select(ci => ci.Technics.Price * ci.Quantity).Sum();
        }
    }
}
