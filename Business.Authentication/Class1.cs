using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Authentication;

namespace Business.Authentication
{
    public class Class1
    {
        public Context Ctx=new Context();

        public Register CheckLogin(Register rg)
        {
            return Ctx.Registers.FirstOrDefault(x => x.UserName == rg.UserName && x.Password == rg.Password);
        }

        public List<Register> GetList()
        {
            return Ctx.Registers.ToList();
        }

        public void RegisterUser(Register rg)
        {
            Register register=new Register()
            {
                UserName = rg.UserName,
                Email = rg.Email,
                Password = rg.Password,
                Role = rg.Role
            };
            Ctx.Registers.Add(register);
            Ctx.SaveChanges();
        }


    }
}
