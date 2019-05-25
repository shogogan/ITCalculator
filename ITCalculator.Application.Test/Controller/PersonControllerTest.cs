using Domain.Models;
using ITCalculator.Application.Test.Service;
using ITCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ITCalculator.Application.Test.Controller
{
    public class PersonControllerTest
    {
        PersonController controller = new PersonController(new PersonMockService());

        [Fact]
        public void AddPerson_should_returnBadRequest_because_noPersonName()
        {
            var invalidPerson = new Person();
            var actionResult = controller.AddPerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void AddPerson_should_returnBadRequest_because_noCPF()
        {
            var invalidPerson = new Person
            {
                PersonName = ""
            };
            var actionResult = controller.AddPerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void AddPerson_should_returnBadRequest_because_noMonthlyGrossRevenue()
        {
            var invalidPerson = new Person
            {
                PersonName = "",
                CPF = "",
                DependentsAmount = 0
            };
            var actionResult = controller.AddPerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void AddPerson_should_returnBadRequest_because_noDependentsAmount()
        {
            var invalidPerson = new Person
            {
                PersonName = "",
                CPF = "",
                MonthlyGrossRevenue = 0
            };
            var actionResult = controller.AddPerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void AddPerson_should_returnOk_because_validUser()
        {
            var validPerson = new Person
            {
                PersonName = "",
                CPF = "",
                MonthlyGrossRevenue = 0,
                DependentsAmount = 0
            };
            var actionResult = controller.AddPerson(validPerson);

            Assert.Equal(typeof(OkObjectResult), actionResult.GetType());
        }
        
        [Fact]
        public void UpdatePerson_should_returnBadRequest_because_noPersonId()
        {
            var invalidPerson = new Person
            {
                PersonName = "",
                CPF = "",
                MonthlyGrossRevenue = 0,
                DependentsAmount = 0
            };
            var actionResult = controller.UpdatePerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }
        
        [Fact]
        public void UpdatePerson_should_returnBadRequest_because_noPersonName()
        {
            var invalidPerson = new Person
            {
                PersonId = 1,
                CPF = "",
                MonthlyGrossRevenue = 0,
                DependentsAmount = 0
            };
            var actionResult = controller.UpdatePerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void UpdatePerson_should_returnBadRequest_because_noCPF()
        {
            var invalidPerson = new Person
            {
                PersonId = 1,
                PersonName = "",
                MonthlyGrossRevenue = 0,
                DependentsAmount = 0
            };
            var actionResult = controller.UpdatePerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void UpdatePerson_should_returnBadRequest_because_noMonthlyGrossRevenue()
        {
            var invalidPerson = new Person
            {
                PersonId = 1,
                PersonName = "",
                CPF = "",
                DependentsAmount = 0
            };
            var actionResult = controller.UpdatePerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void UpdatePerson_should_returnBadRequest_because_noDependentsAmount()
        {
            var invalidPerson = new Person
            {
                PersonId = 1,
                PersonName = "",
                CPF = "",
                MonthlyGrossRevenue = 0
            };
            var actionResult = controller.UpdatePerson(invalidPerson);

            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
        }

        [Fact]
        public void UpdatePerson_should_returnOk_because_validUser()
        {
            var validPerson = new Person
            {
                PersonId = 1,
                PersonName = "",
                CPF = "",
                MonthlyGrossRevenue = 0,
                DependentsAmount = 0
            };
            var actionResult = controller.UpdatePerson(validPerson);

            Assert.Equal(typeof(OkObjectResult), actionResult.GetType());
        }
    }
}