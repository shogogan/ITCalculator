using System;
using Domain.Service;
using ITCalculator.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ITCalculator.Controllers
{
    [Route("api/[controller]")]
    public class TaxController
    {
        private readonly ITaxService _personService;

        public TaxController(ITaxService taxService)
        {
            _personService = taxService;
        }

        [HttpPost]
        public IActionResult ListPersonTax([FromBody] ListPersonTaxDto dto)
        {
            try
            {
                var listPeopleTax = _personService.ListPeopleTax(dto.MinimumWage);
                return new OkObjectResult(listPeopleTax);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}