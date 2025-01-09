namespace BirdScout.Server.Models
{
    public class RegisterModel
    {
        public int OpMode { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicture { get; set; }
    }
}
