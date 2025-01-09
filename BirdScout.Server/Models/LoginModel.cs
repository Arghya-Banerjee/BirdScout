namespace BirdScout.Server.Models
{
    public class LoginModel
    {
        public int OpMode { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
