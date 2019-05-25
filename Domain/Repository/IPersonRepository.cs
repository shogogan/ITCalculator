using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repository
{
    public interface IPersonRepository
    {
        Person InsertPerson(Person person);
        void DeletePerson(int personId);
        IEnumerable<Person> GetAllPeople();
        ListResponse<IEnumerable<Person>> ListPeople(int page, int amount);
        Person GetPerson(int personId);
        Person UpdatePerson(Person person);
    }
}