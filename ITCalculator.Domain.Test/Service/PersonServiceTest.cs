using System;
using Domain.Models;
using Domain.Service;
using ITCalculator.Test.Repository;
using Xunit;

namespace ITCalculator.Test.Service
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService = new PersonService(new PersonMockRepository());
        
        [Fact]
        public void InsertPerson_should_throwError()
        {
            var invalidPerson = new Person
            {
                CPF = "239847"
            };

            Assert.Throws<ArgumentException>(() => _personService.InsertPerson(invalidPerson));
        }

        [Fact]
        public void InsertPerson_should_returnPerson()
        {
            var validPerson = new Person
            {
                PersonName = "mockName",
                CPF = "643.185.010-60",
                DependentsAmount = 1,
                MonthlyGrossRevenue = 1000
            };
            var insertPerson = _personService.InsertPerson(validPerson);
            Assert.NotNull(insertPerson);
        }
        [Fact]
        public void UpdatePerson_should_throwError()
        {
            var invalidPerson = new Person
            {
                CPF = "239847"
            };

            Assert.Throws<ArgumentException>(() => _personService.UpdatePerson(invalidPerson));
        }

        [Fact]
        public void UpdatePerson_should_returnPerson()
        {
            var validPerson = new Person
            {
                PersonName = "mockName",
                CPF = "643.185.010-60",
                DependentsAmount = 1,
                MonthlyGrossRevenue = 1000
            };
            var insertPerson = _personService.UpdatePerson(validPerson);
            Assert.NotNull(insertPerson);
        }

        [Fact]
        public void DeletePerson_should_runNormally()
        {
            _personService.DeletePerson(1);
        }

        [Fact]
        public void ListPerson_should_returnPersonList()
        {
            var listResponse = _personService.ListPerson(0, 10);
            Assert.NotNull(listResponse);
        }

        [Fact]
        public void GetPerson_should_returnPerson()
        {
            var person = _personService.GetPerson(1);
            Assert.NotNull(person);
        }
        
        

//            Person InsertPerson(Person person);
//        void DeletePerson(int personId);
//        ListResponse<IEnumerable<Person>> ListPerson(int page, int amount);
//        Person GetPerson(int personId);
//        Person UpdatePerson(Person person);
    }
}