using WebAPI.DataModel.ResponseModel;

namespace WebAPI.Service.User
{
    public interface IUserService
    {
        public Task<JsonModel> GetUserByIdAsync(int id);
    }
}
