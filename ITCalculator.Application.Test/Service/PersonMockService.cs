using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Service;

namespace ITCalculator.Application.Test.Service
{
    public class PersonMockService
        : IPersonService
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

        public ListResponse<IEnumerable<Person>> ListPerson(int page, int amount)
        {
            return new ListResponse<IEnumerable<Person>>
            {
                Total = 4,
                Pages = (int) Math.Round((double) (4 / amount), MidpointRounding.AwayFromZero),
                Result = People
                    .Skip(amount * page)
                    .Take(amount)
                    .ToList()
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