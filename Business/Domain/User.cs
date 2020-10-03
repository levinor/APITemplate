using Levinor.Business.Utils;

namespace Levinor.Business.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public UserType Role { get; set; }             
        public Password Password { get; set; }
        public User Updater { get; set; }
        public User Supervisor { get; set; }
        public bool Active { get; set; }
    }
}
