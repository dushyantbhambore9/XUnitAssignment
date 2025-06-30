using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<JsonModel> IsEmailRegisteredAsync(RegisterDto registerDto)
        {
            return await _authRepo.IsEmailRegisteredAsync(registerDto);
        }

        public JsonModel IsValidPassword(string Password)
        {
            return _authRepo.IsValidPassword(Password);
        }

        public Task<JsonModel> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
