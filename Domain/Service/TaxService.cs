using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repository;

namespace Domain.Service
{
    public class TaxService : ITaxService
    {
        private readonly IPersonRepository _repository;

        public TaxService(IPersonRepository repository)
        {
            _repository = repository;
        }

        /*
         * NOTA: Esta funcionalidade foi feita sendo pensada como se o usuário retornasse uma lista de todos os contribuintes
         * para um relatório.
         * Se a idéia for tornar ele paginável, eu faria mais uma tabela para salvar o valor do salario minimo e uma referenciando
         * a de Contribuintes salvando os valores, já que assim caso não seja modificado o salario minimo, não precisaria gastar
         * poder computacional para recalcular a cada pagina carregada por exemplo.
         */
        public IEnumerable<TaxResponse> ListPeopleTax(double minimumWage)
        {
            var people = _repository.GetAllPeople();

            return (from person in people
                    let liquidRevenue = CalculateLiquidRevenue(minimumWage, person)
                    let taxValue = CalculateTax(minimumWage, liquidRevenue)
                    select new TaxResponse
                    {
                        CPF = person.CPF,
                        PersonName = person.PersonName,
                        TaxValue = taxValue
                    })
                .OrderBy(x => x.TaxValue)
                .ThenBy(x => x.PersonName)
                .ToList();
        }

        public static double CalculateLiquidRevenue(double minimumWage, Person person)
        {
            if (person.MonthlyGrossRevenue == null)
            {
                throw new ArgumentException("Renda bruta mensal do contribuinte é obrigatório");
            }

            if (person.DependentsAmount == null)
            {
                throw new ArgumentException("Quantidade de dependentes do contribuinte é obrigatório");
            }

            return person.MonthlyGrossRevenue.Value - ((minimumWage * 0.05) * person.DependentsAmount.Value);
        }

        public static double CalculateTax(double minimumWage, double liquidRevenue)
        {
            var minimumWageAmount = liquidRevenue / minimumWage;
            var taxPercentage = GetTaxPercentage(minimumWageAmount);

            return Math.Round(liquidRevenue * taxPercentage, 2, MidpointRounding.ToEven);
        }

        public static double GetTaxPercentage(double minimumWageAmount)
        {
            var taxPercentage = 0.0d;
            if (!(minimumWageAmount > 2)) return taxPercentage;
            if (minimumWageAmount > 2)
                taxPercentage += 0.075d;
            if (minimumWageAmount > 4)
                taxPercentage += 0.075d;
            if (minimumWageAmount > 5)
                taxPercentage += 0.075d;
            if (minimumWageAmount > 7)
                taxPercentage += 0.050d;

            return Math.Round(taxPercentage, 3, MidpointRounding.ToEven);
        }
    }
}