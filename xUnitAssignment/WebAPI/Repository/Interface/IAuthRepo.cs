using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;

namespace WebAPI.Repository.Interface;

public interface IAuthRepo
{
    public Task<JsonModel> IsEmailRegisteredAsync(RegisterDto registerDto);

    public JsonModel IsValidPassword(string Password);
    public Task<JsonModel> LoginAsync(LoginDto loginDto);
}

