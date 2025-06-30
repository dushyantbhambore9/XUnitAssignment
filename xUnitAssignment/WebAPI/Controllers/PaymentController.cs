using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Service.Payment;


[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<JsonModel> MakePayment([FromBody] PaymentDto paymentDto)
    {
        return await _paymentService.ProcessPaymentAsync(paymentDto);
    }
}
