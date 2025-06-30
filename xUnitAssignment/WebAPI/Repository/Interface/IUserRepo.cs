using WebAPI.DataModel.Entity;
using WebAPI.DataModel.ResponseModel;

namespace WebAPI.Repository.Interface
{
    public interface IUserRepo
    {
        public Task<JsonModel> GetUserByIdAsync(int id);
    }
}
