using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Repository;
using ITCalculator.Utils;

namespace Domain.Service
{
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }
        
        public Person InsertPerson(Person person)
        {
            if (!ValidatorUtils.IsCpf(person.CPF))
            {
                throw new ArgumentException("CPF Inválido.");
            }
            

            return _repository.InsertPerson(person);
        }

        public Person UpdatePerson(Person person)
        {
            if (!ValidatorUtils.IsCpf(person.CPF))
            {
                throw new ArgumentException("CPF Inválido.");
            }

            return _repository.UpdatePerson(person);
        }

        public void DeletePerson(int personId)
        {
            _repository.DeletePerson(personId);
        }

        public ListResponse<IEnumerable<Person>> ListPerson(int page, int amount)
        {
            return _repository.ListPeople(page, amount);
        }

        public Person GetPerson(int personId)
        {
            return _repository.GetPerson(personId);
        }
    }
}