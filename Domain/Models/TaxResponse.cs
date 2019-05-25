using System;

namespace Domain.Models
{
    public class TaxResponse
    {
        public string PersonName { get; set; }
        public string CPF { get; set; }
        public double TaxValue { get; set; }
    }
}