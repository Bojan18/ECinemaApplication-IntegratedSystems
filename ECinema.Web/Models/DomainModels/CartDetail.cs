using System.ComponentModel.DataAnnotations;

namespace ECinema.Web.Models.DomainModels
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        public ShoppingCart shoppingCart { get; set; }

        [Required]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
