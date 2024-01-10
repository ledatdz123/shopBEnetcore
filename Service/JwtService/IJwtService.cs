using shopEcomerceExBE.Model;

namespace shopEcomerceExBE.Service.JwtService
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
