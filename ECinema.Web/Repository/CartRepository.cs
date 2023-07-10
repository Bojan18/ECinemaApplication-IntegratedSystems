using ECinema.Web.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECinema.Web.Repository
{
    public interface CartRepository
    {
        Task<int> AddItem(int ticketId, int quantity);
        Task<int> DeleteItem(int ticketId);
        public Task<ShoppingCart> GetUserCart();
        public Task<int> GetCartItemCount(string userId = "");
    }
}
