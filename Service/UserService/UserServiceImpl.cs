using shopEcomerceExBE.Common.ResponseMessage;
using shopEcomerceExBE.Model;
using shopEcomerceExBE.Repos.UserRepos;
using shopEcomerceExBE.Service.JwtService;
using System.Reflection;
using System.Security.Claims;

namespace shopEcomerceExBE.Service.UserService
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepos _repos;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserServiceImpl(IUserRepos repos, IJwtService jwtService, IHttpContextAccessor httpContextAccessor) {  
            _repos = repos;
            _jwtService = jwtService;
            _contextAccessor = httpContextAccessor;
        }

        public ResponseMessage GetUserLogin()
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                string message;
                object data;
                if (_contextAccessor == null)
                {
                    message = "Không có user";
                    data= null;
                }
                else
                {
                    message = "get user thành công";
                    var email = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                    data = _repos.GetUserByEmail(email);
                }
                rp.data = data;
                rp.message = message;
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage LoginUser(UserLogin user)
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                string message;
                object data;
                var userLogin = _repos.GetUserByEmail(user.Email);

                // Kiểm tra xem người dùng tồn tại và mật khẩu có khớp hay không
                if (userLogin == null)
                {
                    message = "User not found with email";
                    data= new object();
                }
                else if(userLogin !=null && !BCrypt.Net.BCrypt.Verify(user.Password, userLogin.Password_Hash))
                {
                    message = "Sai mật khẩu";
                    data = new object();
                }
                else
                {
                    message = "Login thành công";
                    var token = _jwtService.GenerateJwtToken(userLogin);
                    data = token;
                }
                rp.status = MessageStatus.success;
                rp.data = data;
                rp.message = message;
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage RegisterUser(ApplicationUser user)
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password_Hash);
                string res=_repos.CreateUserAsync(user.User_Name,user.Email, hashedPassword);
                rp.status = MessageStatus.success;
                rp.data = res;
                rp.message = "Thêm mới user thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }
    }
}
