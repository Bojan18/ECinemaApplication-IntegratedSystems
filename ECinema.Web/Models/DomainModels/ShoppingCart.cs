using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECinema.Web.Models.DomainModels
{
    public class ShoppingCart
    {
        internal string userId;

        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool isDeleted { get; set; } = false;
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
