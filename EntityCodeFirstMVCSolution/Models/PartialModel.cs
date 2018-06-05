using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityCodeFirstMVCSolution.Models
{
    public class PartialModel
    {
    }

    public partial class PartialModel2
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

    }
    public partial class PartialModel2
    {
        public List<PartialModel2> PartialModel { get; set; }

    }
}