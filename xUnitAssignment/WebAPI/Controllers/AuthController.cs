using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Service.Auth;
using WebAPI.Service.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<JsonModel> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return new JsonModel(500, "User not found", null);
            }
            return await _authService.IsEmailRegisteredAsync(registerDto);
        }


    }
}
