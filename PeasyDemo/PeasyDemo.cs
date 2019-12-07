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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string SSN { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
    }
}
