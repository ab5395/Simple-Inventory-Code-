using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Authentication
{
    class Class2
    {
        public Context Ctx=new Context();
        

        public void SList()
        {
            foreach (var data in Ctx.Registers)
            {
                Console.WriteLine(data.Email);
            }
        }
    }
}
