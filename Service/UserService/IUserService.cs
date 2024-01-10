using shopEcomerceExBE.Common.ResponseMessage;
using shopEcomerceExBE.Model;

namespace shopEcomerceExBE.Service.UserService
{
    public interface IUserService
    {
        public ResponseMessage RegisterUser(ApplicationUser user);
        public ResponseMessage LoginUser(UserLogin user);
    }
}
