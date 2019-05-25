using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repository;
using Repository.Config;
using Repository.Entities;

namespace Repository.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public Person InsertPerson(Person person)
        {
            using (var ctx = new DbContext())
            {
                if (ctx.People.FirstOrDefault(x => x.CPF == person.CPF) != null)
                {
                    throw new ArgumentException(
                        "Não foi possível incluir o contribuinte, Motivo: Já existe um contribuinte com este CPF.");
                }

                if (person.MonthlyGrossRevenue == null)
                {
                    throw new ArgumentException("Renda bruta mensal do contribuinte é obrigatório");
                }

                if (person.DependentsAmount == null)
                {
                    throw new ArgumentException("Quantidade de dependentes do contribuinte é obrigatório");
                }

                var personEntity = new PersonEntity
                {
                    CPF = person.CPF,
                    PersonName = person.PersonName,
                    DependentsAmount = person.DependentsAmount.Value,
                    MonthlyGrossRevenue = person.MonthlyGrossRevenue.Value
                };
                ctx.People.Add(personEntity);
                ctx.SaveChanges();

                return new Person
                {
                    PersonId = person.PersonId,
                    CPF = person.CPF,
                    PersonName = person.PersonName,
                    DependentsAmount = person.DependentsAmount,
                    MonthlyGrossRevenue = person.MonthlyGrossRevenue
                };
            }
        }

        public Person UpdatePerson(Person person)
        {
            using (var ctx = new DbContext())
            {
                if (ctx.People.FirstOrDefault(x => x.CPF == person.CPF && x.PersonId != person.PersonId) != null)
                {
                    throw new ArgumentException(
                        "Não foi possível incluir o contribuinte, Motivo: Já existe um contribuinte com este CPF.");
                }

                if (person.PersonId == null)
                {
                    throw new ArgumentException("Id do contribuinte é obrigatório");
                }

                if (person.MonthlyGrossRevenue == null)
                {
                    throw new ArgumentException("Renda bruta mensal do contribuinte é obrigatório");
                }

                if (person.DependentsAmount == null)
                {
                    throw new ArgumentException("Quantidade de dependentes do contribuinte é obrigatório");
                }


                var personEntity = new PersonEntity
                {
                    PersonId = person.PersonId.Value,
                    CPF = person.CPF,
                    PersonName = person.PersonName,
                    DependentsAmount = person.DependentsAmount.Value,
                    MonthlyGrossRevenue = person.MonthlyGrossRevenue.Value
                };
                ctx.People.Update(personEntity);
                ctx.SaveChanges();

                return new Person
                {
                    PersonId = person.PersonId,
                    CPF = person.CPF,
                    PersonName = person.PersonName,
                    DependentsAmount = person.DependentsAmount,
                    MonthlyGrossRevenue = person.MonthlyGrossRevenue
                };
            }
        }

        public void DeletePerson(int personId)
        {
            using (var ctx = new DbContext())
            {
                var personEntity = ctx.People.Find(personId);
                if (personEntity == null)
                {
                    throw new ArgumentException(
                        "Não foi possível excluir o contribuinte, motivo: Contribuinte não encontrado");
                }

                ctx.People.Remove(personEntity);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Person> GetAllPeople()
        {
            using (var ctx = new DbContext())
            {
                return ctx.People
                    .Select(person => new Person
                    {
                        PersonId = person.PersonId,
                        CPF = person.CPF,
                        PersonName = person.PersonName,
                        DependentsAmount = person.DependentsAmount,
                        MonthlyGrossRevenue = person.MonthlyGrossRevenue
                    }).ToList();
            }
        }

        public ListResponse<IEnumerable<Person>> ListPeople(int page, int amount)
        {
            using (var ctx = new DbContext())
            {
                var count = ctx.People.Count();
                var pageAmount = (int) Math.Ceiling((double)count / amount);
                var people = ctx.People
                    .Skip(page * amount)
                    .Take(amount)
                    .Select(person => new Person
                    {
                        PersonId = person.PersonId,
                        CPF = person.CPF,
                        PersonName = person.PersonName,
                        DependentsAmount = person.DependentsAmount,
                        MonthlyGrossRevenue = person.MonthlyGrossRevenue
                    }).ToList();
                return new ListResponse<IEnumerable<Person>>
                {
                    Pages = pageAmount,
                    Result = people,
                    Total = count
                };
            }
        }

        public Person GetPerson(int personId)
        {
            using (var ctx = new DbContext())
            {
                var personEntity = ctx.People.Find(personId);

                if (personEntity == null)
                {
                    throw new ArgumentException(
                        "Não foi possível encontrar o contribuinte.");
                }

                return new Person
                {
                    PersonId = personEntity.PersonId,
                    CPF = personEntity.CPF,
                    PersonName = personEntity.PersonName,
                    DependentsAmount = personEntity.DependentsAmount,
                    MonthlyGrossRevenue = personEntity.MonthlyGrossRevenue
                };
            }
        }
    }
}