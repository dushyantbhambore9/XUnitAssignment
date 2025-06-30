using Microsoft.EntityFrameworkCore;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implementation
{
    public class PaymentRepo : IPaymentRespo
    {
        private readonly OrderDbContext _orderDbContext;

        public PaymentRepo(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<JsonModel> ProcessPaymentAsync(PaymentDto paymentDto)
        {

            var CreateOrder = await _orderDbContext.Order.FirstOrDefaultAsync(x => x.OrderId == paymentDto.OrderId);

            if (paymentDto == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            if (CreateOrder.IsPaid == true)
            {
                throw new InvalidOperationException("Order already paid");
            }

            CreateOrder.IsPaid = true;
            return new JsonModel(200, "Payment Processed Successfully", CreateOrder);
        }
    }
}
