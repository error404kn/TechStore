using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class CartController : Controller
    {
        private readonly TechStoreContext _context;
        private readonly Cart _cart;

        public CartController(TechStoreContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;
            return View(_cart);
        }
        /*dd to cart*/
        public IActionResult AddToCart(int id) 
        { 
            var selectedTechnics = GetTechnicsById(id);

            if (selectedTechnics != null) 
            {
                _cart.AddToCart(selectedTechnics, 1);
            }
            return RedirectToAction("Index", "Home");
        }



       /* Remove from cart*/
        public IActionResult RemoveFromCart(int id)
        {
            var selectedTechnics = GetTechnicsById(id);

            if(selectedTechnics != null)
            {
                _cart.RemoveFromCart(selectedTechnics);
            }
            return RedirectToAction("Index");
        }

        /*Reduce Quantity*/
        public IActionResult ReduceQuantity(int id)
        {
            var selectedTechnics = GetTechnicsById(id);

            if (selectedTechnics != null)
            {
                _cart.ReduceQuantity(selectedTechnics);
            }
            return RedirectToAction("Index");
        }

        /*Increase Quantity*/
        public IActionResult IncreaseQuantity(int id)
        {
            var selectedTechnics = GetTechnicsById(id);

            if (selectedTechnics != null)
            {
                _cart.IncreaseQuantity(selectedTechnics);
            }
            return RedirectToAction("Index");
        }

        /*Clear cart items*/
        public IActionResult ClearCart()
        {
            _cart.ClearCart();
            return RedirectToAction("Index");
        }

        public Technics GetTechnicsById(int id)
        {
            return _context.Technics.FirstOrDefault(b => b.Id == id);
        }
    }
}
