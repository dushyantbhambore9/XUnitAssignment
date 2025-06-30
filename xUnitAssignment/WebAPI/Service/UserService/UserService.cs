using WebAPI.DataModel.ResponseModel;
using WebAPI.Repository.Interface;

namespace WebAPI.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<JsonModel> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetUserByIdAsync(id);
        }
    }
}
