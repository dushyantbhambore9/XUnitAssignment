using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Service.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<JsonModel> GetUserById([FromBody] int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
    }
}
