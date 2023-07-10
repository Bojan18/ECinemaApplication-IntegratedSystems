using System.ComponentModel.DataAnnotations;

namespace ECinema.Web.Models.DomainModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public Order Order { get; set; }

    }
}
