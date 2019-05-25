using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Service;
using ITCalculator.Test.Repository;
using Xunit;

namespace ITCalculator.Test.Service
{
    public class TaxServiceTest
    {
        [Theory]
        [InlineData(1d)]
        [InlineData(1.5d)]
        [InlineData(2d)]
        [InlineData(0d)]
        public void GetTaxPercentage_should_returnZero(double minimumWageAmount)
        {
            Assert.Equal(0d, TaxService.GetTaxPercentage(minimumWageAmount));
        }

        [Theory]
        [InlineData(2.1d)]
        [InlineData(4d)]
        [InlineData(3.5d)]
        public void GetTaxPercentage_should_returnFirstLevel(double minimumWageAmount)
        {
            Assert.Equal(0.075d, TaxService.GetTaxPercentage(minimumWageAmount));
        }

        [Theory]
        [InlineData(4.1d)]
        [InlineData(4.5d)]
        [InlineData(5d)]
        public void GetTaxPercentage_should_returnSecondLevel(double minimumWageAmount)
        {
            Assert.Equal(0.15d, TaxService.GetTaxPercentage(minimumWageAmount));
        }

        [Theory]
        [InlineData(5.1d)]
        [InlineData(6d)]
        [InlineData(7d)]
        public void GetTaxPercentage_should_returnThirdLevel(double minimumWageAmount)
        {
            Assert.Equal(0.225d, TaxService.GetTaxPercentage(minimumWageAmount));
        }

        [Theory]
        [InlineData(7.0000001d)]
        [InlineData(8d)]
        [InlineData(1000000d)]
        public void GetTaxPercentage_should_returnForthLevel(double minimumWageAmount)
        {
            Assert.Equal(0.275d, TaxService.GetTaxPercentage(minimumWageAmount));
        }

        [Fact]
        public void CalculateLiquidRevenue_should_returnFullGrossRevenue()
        {
            var person = new Person
            {
                DependentsAmount = 0,
                MonthlyGrossRevenue = 1000
            };
            Assert.Equal(person.MonthlyGrossRevenue, TaxService.CalculateLiquidRevenue(1000, person));
        }

        [Fact]
        public void CalculateLiquidRevenue_should_return995()
        {
            var person = new Person
            {
                DependentsAmount = 1,
                MonthlyGrossRevenue = 1000
            };
            Assert.Equal(995, TaxService.CalculateLiquidRevenue(100, person));
        }

        [Fact]
        public void CalculateLiquidRevenue_should_returnZero()
        {
            var person = new Person
            {
                DependentsAmount = 20,
                MonthlyGrossRevenue = 1000
            };
            Assert.Equal(0, TaxService.CalculateLiquidRevenue(1000, person));
        }

        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(1000, 2000)]
        [InlineData(1500, 3000)]
        [InlineData(2000, 0)]
        [InlineData(1250, 2500)]
        public void CalculateTax_should_returnZero(double minimumWage, double liquidRevenue)
        {
            Assert.Equal(0, TaxService.CalculateTax(minimumWage, liquidRevenue));
        }

        [Theory]
        [InlineData(1000, 3000)]
        public void CalculateTax_should_return225(double minimumWage, double liquidRevenue)
        {
            Assert.Equal(225, TaxService.CalculateTax(minimumWage, liquidRevenue));
        }

        [Theory]
        [InlineData(700, 3000)]
        public void CalculateTax_should_return450(double minimumWage, double liquidRevenue)
        {
            Assert.Equal(450, TaxService.CalculateTax(minimumWage, liquidRevenue));
        }

        [Theory]
        [InlineData(500, 3000)]
        public void CalculateTax_should_return675(double minimumWage, double liquidRevenue)
        {
            Assert.Equal(675, TaxService.CalculateTax(minimumWage, liquidRevenue));
        }

        [Theory]
        [InlineData(100, 3000)]
        public void CalculateTax_should_return825(double minimumWage, double liquidRevenue)
        {
            Assert.Equal(825, TaxService.CalculateTax(minimumWage, liquidRevenue));
        }

        [Fact]
        public void ListPeopleTax_should_returnPeopleTax()
        {
            var taxService = new TaxService(new PersonMockRepository());

            var taxList = taxService.ListPeopleTax(1000);

            var expectedResponses = new List<TaxResponse>
            {
                new TaxResponse {TaxValue = 0, PersonName = "Person 1"},
                new TaxResponse {TaxValue = 0, PersonName = "Person 4"},
                new TaxResponse {TaxValue = 712.5, PersonName = "Person 2"},
                new TaxResponse {TaxValue = 13750, PersonName = "Person 3"}
            };

            var taxResponses = taxList.ToList();
            foreach (var taxResponse in taxResponses)
            {
                var expectedResponse = expectedResponses[taxResponses.IndexOf(taxResponse)];
                Assert.Equal(expectedResponse.PersonName, taxResponse.PersonName);
                Assert.Equal(expectedResponse.TaxValue, taxResponse.TaxValue);
            }
        }
    }
}