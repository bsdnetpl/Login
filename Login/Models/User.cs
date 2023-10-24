using FluentValidation;
using Microsoft.Identity.Client;

namespace Login.Models
{
    public class User
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Phone { set; get; }
        public DateTime DateCreate { set; get; }
        public bool Activate { set; get; } = true;
        public string Role { set; get; } = "User";
    }


}
