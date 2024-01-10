using shopEcomerceExBE.Model;

namespace shopEcomerceExBE.Repos.UserRepos
{
    public interface IUserRepos
    {
        public string CreateUserAsync(string p_username,string p_email, string p_password);
        public ApplicationUser GetUserByEmail(string email);
        public List<ApplicationUser> GetAllUsers();
    }
}
