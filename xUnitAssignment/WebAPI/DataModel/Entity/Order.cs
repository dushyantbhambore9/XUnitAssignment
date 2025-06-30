using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataModel.Entity
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int OrderItem { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
