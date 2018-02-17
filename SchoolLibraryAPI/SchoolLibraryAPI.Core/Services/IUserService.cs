using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.Core.Services;

namespace SchoolLibraryAPI.Core
{
    public interface IUserService : IService<UserModel>
    {        
        new UserAddEditResult Update(UserModel model);
        TokenModel GetToken(string userName, string password);
    }
}