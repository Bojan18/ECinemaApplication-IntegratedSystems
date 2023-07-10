using System.ComponentModel.DataAnnotations;

namespace ECinema.Web.Models.DomainModels
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }
        [Required]
        public int StatusId { get; set; }

    }
}
