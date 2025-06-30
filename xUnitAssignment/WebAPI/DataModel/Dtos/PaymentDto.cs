using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.DataModel.Entity;

namespace WebAPI.DataModel.Dtos
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderItem { get; set; }
        public bool IsPaid { get; set; }

    }
}
