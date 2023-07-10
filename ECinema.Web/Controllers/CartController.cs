using ECinema.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECinema.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly CartRepository cartRepository;

        public CartController(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }


        [HttpGet]
        [Route("/Cart/Create")]
        public async Task<IActionResult> Create(int ticketId, int quantity = 1, int redirect = 0)
        {

            var cartCount = await cartRepository.AddItem(ticketId, quantity);


            if (redirect == 0)
            {
                return Ok(cartCount);
            }

            return RedirectToAction("GetUserCart");            
        }

        public async Task<IActionResult> Delete(int ticketId)
        {
            var cartCount = await cartRepository.DeleteItem(ticketId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRepository.GetUserCart();

            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemsInCart()
        {
            int cartItem = await cartRepository.GetCartItemCount();
            return Ok(cartItem);
        }


    }
}
