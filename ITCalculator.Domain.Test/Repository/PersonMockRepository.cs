using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repository;

namespace ITCalculator.Test.Repository
{
    public class PersonMockRepository : IPersonRepository
    {
        private IEnumerable<Person> People { get; } = new List<Person>
        {
            new Person {PersonName = "Person 1", DependentsAmount = 1, MonthlyGrossRevenue = 1000},
            new Person {PersonName = "Person 2", DependentsAmount = 5, MonthlyGrossRevenue = 5000},
            new Person {PersonName = "Person 3", DependentsAmount = 0, MonthlyGrossRevenue = 50000},
            new Person {PersonName = "Person 4", DependentsAmount = 3, MonthlyGrossRevenue = 100}
        };

        public Person InsertPerson(Person person)
        {
            return person;
        }

        public void DeletePerson(int personId)
        {
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return People;
        }

        public ListResponse<IEnumerable<Person>> ListPeople(int page, int amount)
        {
            return new ListResponse<IEnumerable<Person>>
            {
                Pages = 1,
                Result = People.Take(amount).Skip(amount * page).ToList(),
                Total = 4
            };
        }

        public Person GetPerson(int personId)
        {
            return People.First();
        }

        public Person UpdatePerson(Person person)
        {
            return person;
        }
    }
}