using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECinema.Web.Models.DomainModels
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool isDeleted { get; set; } = false;

        public List<OrderDetail> OrderDetail { get; set; }
    }
}
