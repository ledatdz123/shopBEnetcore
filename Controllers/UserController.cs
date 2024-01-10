using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopEcomerceExBE.Model;
using shopEcomerceExBE.Service.UserService;

namespace shopEcomerceExBE.Controllers
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
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin assessors)
        {
            var register = _userService.LoginUser(assessors);
            return Ok(register);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] ApplicationUser assessors)
        {
            var register = _userService.RegisterUser(assessors);
            return Ok(register);
        }
    }
}
