using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Authentication
{
    public class Context:DbContext
    {

        public Context():base("AuthenticationConnectionString")
        {
        }

        public DbSet<Register> Registers { get; set; }
    }
}
