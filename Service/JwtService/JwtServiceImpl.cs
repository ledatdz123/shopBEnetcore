using Microsoft.IdentityModel.Tokens;
using shopEcomerceExBE.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace shopEcomerceExBE.Service.JwtService
{
    public class JwtServiceImpl : IJwtService
    {
        private readonly string _secretKey; // Thay thế bằng khóa bí mật thực sự của bạn
        private readonly string _issuer = "your_issuer_here"; // Thay thế bằng người phát hành thực sự của bạn
        public JwtServiceImpl()
        {
            // Tạo một khóa bí mật ngẫu nhiên (32 byte)
            byte[] keyBytes = new byte[32];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            // Chuyển đổi khóa thành chuỗi Base64
            _secretKey = Convert.ToBase64String(keyBytes);
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Thời gian hết hạn của token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
