using shopEcomerceExBE.Common.BaseService;
using shopEcomerceExBE.Model;

namespace shopEcomerceExBE.Repos.UserRepos
{
    public class UserReposImpl : IUserRepos
    {
        private readonly IBaseService _baseService;
        public UserReposImpl(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        public string CreateUserAsync(string p_username, string p_email, string p_password)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("register_user");
                obj.AddParameter("@username", p_username);
                obj.AddParameter("@email", p_email);
                obj.AddParameter("@password", p_password);
                return obj.ExecStoreToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_all_users");
                return obj.ExecStoreProcedureToList<ApplicationUser>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_user_by_email");
                obj.AddParameter("@email", email);
                return obj.ExecStoreRefToObject<ApplicationUser>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }
    }
}
