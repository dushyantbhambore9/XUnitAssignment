using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;

namespace WebAPI.Service.Payment
{
    public interface IPaymentService
    {
        public Task<JsonModel> ProcessPaymentAsync(PaymentDto paymentDto);
    }
}
