using Microsoft.EntityFrameworkCore;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.Dtos;
using WebAPI.DataModel.Entity;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implementation
{
    public class AuthRespo : IAuthRepo
    {
        private readonly OrderDbContext _orderDbContext;

        public AuthRespo(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<JsonModel> IsEmailRegisteredAsync(RegisterDto registerDto)
        {
            var checkEmailExits = await _orderDbContext.User.FirstOrDefaultAsync(x => x.Email == registerDto.Email);

            if (checkEmailExits != null)
            {
                return new JsonModel(400, "Email already exists", new object());
            }

            var passwordValidationResult = IsValidPassword(registerDto.Password);

            if (passwordValidationResult.StatusCode != 200)
            {
                return new JsonModel(passwordValidationResult.StatusCode, passwordValidationResult.message, passwordValidationResult.Data);
            }

            var AddUser = new User
            {
                UserId = registerDto.UserId,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            _orderDbContext.User.Add(AddUser);
            await _orderDbContext.SaveChangesAsync();
            return new JsonModel(200, "User Registered Successfully", AddUser);
        }

        public JsonModel IsValidPassword(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password)) return new JsonModel(400, "Password cannot be empty", false);
            if (Password.Length < 8) return new JsonModel(400, "Password must be at least 8 characters long", false);
            if (!Password.Any(char.IsUpper)) return new JsonModel(400, "Password must contain at least one uppercase letter", false);
            if (!Password.Any(char.IsDigit)) return new JsonModel(400, "Password must contain at least one number", false);

            return new JsonModel(200, "Password is valid", true);
        }

        public Task<JsonModel> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
