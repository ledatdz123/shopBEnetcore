using Microsoft.IdentityModel.Tokens;
using shopEcomerceExBE.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace shopEcomerceExBE.Service.JwtService
{
    public class JwtServiceImpl : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtServiceImpl(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Thời gian hết hạn của token
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
