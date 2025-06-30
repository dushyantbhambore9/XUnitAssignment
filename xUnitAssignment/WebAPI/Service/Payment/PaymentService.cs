using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRespo _paymentRepo;

        public PaymentService(IPaymentRespo paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        public async Task<JsonModel> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            return await _paymentRepo.ProcessPaymentAsync(paymentDto);
        }
    }
}
