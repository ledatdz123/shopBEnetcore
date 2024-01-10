namespace shopEcomerceExBE.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; } // Mật khẩu đã hash
        public object Roles { get; set; }
        /*public List<string> Roles { get; set; } // Danh sách các vai trò người dùng (tùy chọn)

        // Các thông tin khác về người dùng (tùy chọn)

        public ApplicationUser()
        {
            Roles = new List<string>();
        }*/
    }
}
