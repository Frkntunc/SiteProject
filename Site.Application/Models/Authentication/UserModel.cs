using System.Collections.Generic;

namespace Site.Application.Models.Authentication
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public IList<string> Roles { get; set; }
    }
}
