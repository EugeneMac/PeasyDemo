using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public class PersonMockDataProxy : IDataProxy<Person, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            return new[]
            {
                new Person{ID=1, firstName="John", lastName="Doe", birthDate = new DateTime(1985,10,12), SSN = "1111-222-333", City="Washington", Department="IT", Position="CTO", Salary=150000},
                new Person{ID=2, firstName="Mary", lastName="Smith", birthDate = new DateTime(1987,06,10), SSN = "1234-567-890", City="New York", Department="IT", Position="Senior Developer", Salary=100000},
                new Person{ID=3, firstName="Alex", lastName="Black", birthDate = new DateTime(1988,1,2), SSN = "1010-112-113", City="Washington", Department="IT", Position="System Administrator", Salary=80000}
            };
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            return (Task<IEnumerable<Person>>)GetAll();
        }

        public Person GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Person Insert(Person person)
        {
            return new Person() { ID = new Random(300).Next(), firstName = person.firstName };
        }

        public Task<Person> InsertAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Person Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonService : ServiceBase<Person, int>
    {
        public PersonService(IDataProxy<Person, int> dataProxy) : base(dataProxy)
        {
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(Person person, ExecutionContext<Person> context)
        {
            yield return new PersonNameRule(person.firstName, person.lastName);
        }
    }

    public class PersonNameRule : RuleBase
    {
        private string _firstName;
        private string _lastName;

        public PersonNameRule(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        protected override void OnValidate()
        {
            if (!Regex.IsMatch(_firstName,@"\w+") || !Regex.IsMatch(_lastName, @"\w+"))
            {
                Invalidate("Names should contain only letters!");
            }
        }
    }

    public class PersonSSNRule : RuleBase
    {
        private string _ssn;
        private PersonMockDataProxy _dp;

        public PersonSSNRule(string ssn, PersonMockDataProxy dp)
        {
            _ssn = ssn;
            _dp = dp;
        }

        protected override void OnValidate()
        {
            if ((from person in _dp.GetAll() where person.SSN == _ssn select person)!=null)
            {
                Invalidate("SSN of a new person should be unique! A person with this SSN is already in the database!");
            }
        }
    }


}
