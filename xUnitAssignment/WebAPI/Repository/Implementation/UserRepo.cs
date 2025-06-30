using Microsoft.EntityFrameworkCore;
using WebAPI.DataModel.Database;
using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly OrderDbContext _orderDbContext;

        public UserRepo(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<JsonModel> GetUserByIdAsync(int id)
        {
            var checkUser = await _orderDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (checkUser == null)
            {
                return new JsonModel(404, "User not found", null);
            }
            return new JsonModel(200, "User found", checkUser);
        }
    }
}
