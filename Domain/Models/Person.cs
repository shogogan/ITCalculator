namespace Domain.Models
{
    public class Person
    {
        public int? PersonId { get; set; }
        public string CPF { get; set; }
        public string PersonName { get; set; }
        public int? DependentsAmount { get; set; }
        public double? MonthlyGrossRevenue { get; set; }
    }
}