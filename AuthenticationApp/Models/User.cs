using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationApp.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Password { get; set; }
    }

    public class UserClass
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}