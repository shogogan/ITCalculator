using System.Collections.Generic;
using Domain.Models;

namespace Domain.Service
{
    public interface IPersonService
    {
        Person InsertPerson(Person person);
        void DeletePerson(int personId);
        ListResponse<IEnumerable<Person>> ListPerson(int page, int amount);
        Person GetPerson(int personId);
        Person UpdatePerson(Person person);
    }
}