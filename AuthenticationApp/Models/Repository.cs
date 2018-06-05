using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationApp.Models
{
    public static class Repository
    {
        public static List<User> Users = new List<User>() {
            new User() {Email="abc@gmail.com",Roles="Admin,Editor",Password="abcadmin" },
            new User() {Email="xyz@gmail.com",Roles="Editor",Password="xyzeditor" }
        };

        public static User GetUserDetails(User user)
        {
            return Users.FirstOrDefault(u => u.Email.ToLower() == user.Email.ToLower() &&
            u.Password == user.Password);
        }
    }
}