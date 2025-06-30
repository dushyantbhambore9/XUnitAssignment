using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;

namespace WebAPI.Repository.Interface
{
    public interface IPaymentRespo
    {
        public Task<JsonModel> ProcessPaymentAsync(PaymentDto paymentDto);
    }
}
