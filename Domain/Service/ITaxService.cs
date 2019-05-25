using System.Collections;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Service
{
    public interface ITaxService
    {
        IEnumerable<TaxResponse> ListPeopleTax(double minimumWage);
    }
}