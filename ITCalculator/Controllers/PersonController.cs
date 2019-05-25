using System;
using Domain.Models;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ITCalculator.Controllers
{
    [Route("api/[controller]")]
    public class PersonController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("list")]
        public IActionResult ListPerson(int page, int amount)
        {
            try
            {
                return new OkObjectResult(_personService.ListPerson(page, amount));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost("newPerson")]
        public IActionResult AddPerson([FromBody] Person person)
        {
            try
            {
                validateNullFields(person);

                return new OkObjectResult(_personService.InsertPerson(person));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        private static void validateNullFields(Person person, bool idNullable = true)
        {
            if (person.PersonId == null && !idNullable)
            {
                throw new ArgumentException("Id do contribuinte é obrigatório");
            }

            if (person.PersonName == null)
            {
                throw new ArgumentException("Nome do contribuinte é obrigatório");
            }

            if (person.CPF == null)
            {
                throw new ArgumentException("CPF do contribuinte é obrigatório");
            }

            if (person.MonthlyGrossRevenue == null)
            {
                throw new ArgumentException("Renda bruta mensal do contribuinte é obrigatório");
            }

            if (person.DependentsAmount == null)
            {
                throw new ArgumentException("Quantidade de dependentes do contribuinte é obrigatório");
            }
        }

        [HttpDelete("{personId}")]
        public IActionResult DeletePerson(int personId)
        {
            try
            {
                _personService.DeletePerson(personId);
                return new NoContentResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost("updatePerson")]
        public IActionResult UpdatePerson([FromBody] Person person)
        {
            try
            {
                validateNullFields(person, false);

                return new OkObjectResult(_personService.UpdatePerson(person));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet("{personId}")]
        public IActionResult GetPerson(int personId)
        {
            try
            {
                return new OkObjectResult(_personService.GetPerson(personId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.GetType() == typeof(ArgumentException))
                {
                    return new NotFoundObjectResult(e.Message);
                }

                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}