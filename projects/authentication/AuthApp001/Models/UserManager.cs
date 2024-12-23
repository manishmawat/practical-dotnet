using System.Security.Claims;

namespace AuthApp001.Models
{
    public class UserManager
    {
        private static List<User> _accounts;

        static UserManager()
        {
            _accounts = new List<User>
            {
                new User
                {
                    UserName = "alice",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Alice Smith"),
                        new Claim(ClaimTypes.Email, "alice.smith@mydomain.com"),
                        new Claim("admin", "true"),
                    }
                },
                new User
                {
                    UserName = "bob",
                    Password = "password11",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Bob Jones"),
                        new Claim(ClaimTypes.Email, "bob.jones@mydomain.com"),
                        new Claim("admin", "false"),
                    }
                }
            };
        }

        public static User Login(string userName, string password)
        {
            return _accounts.FirstOrDefault(a => a.UserName == userName && a.Password == password);
        }
    }

    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
    }
}