using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Peasy;

namespace PeasyDemo.Tests
{
    
    [TestFixture]
    public class InsertPersonRuleTests
    {
        public static PersonMockDataProxy dataProxy = new PersonMockDataProxy();
        PersonService service = new PersonService(dataProxy);

        [Test]
        public void InsertPersonWithIllegalCharactersName()
        {
            var newPerson = new Person() { firstName = "John*", lastName = "Jameson" };
            var insertResult = service.InsertCommand(newPerson).Execute();

            Assert.AreEqual("Names should contain only letters!", insertResult.Errors.First().ToString());
        }

        [Test]
        public void InsertPerson()
        {
            var newPerson = new Person() { firstName = "John", lastName = "Jameson" };
            var insertResult = service.InsertCommand(newPerson).Execute();

            Assert.AreEqual(true, insertResult.Success);
        }

        [Test]
        public void InsertPersonWithTheSameSSN()
        {
            var rule = new PersonSSNRule("1234-567-890", dataProxy);

            Assert.AreEqual(false, rule.Validate().IsValid);
            Assert.AreEqual("SSN of a new person should be unique! A person with this SSN is already in the database!",
                            rule.Validate().ErrorMessage,
                            "A validation of rule PersonSSNRule should fail because of using already existing SSN! But it was valid!");
        }
    }
}
