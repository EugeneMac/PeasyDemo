using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peasy;

namespace PeasyDemo
{
    public class Person : IDomainObject<int>
    {
        public int ID { get; set; }
    }
}
