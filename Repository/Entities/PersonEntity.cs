using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class PersonEntity
    {
        [Key]
        [Column("id")]
        public int PersonId { get; set; }
        [Column("cpf")]
        public string CPF { get; set; }
        [Column("person_name")]
        public string PersonName { get; set; }
        [Column("dependents_amount")]
        public int DependentsAmount { get; set; }
        [Column("monthly_gross_revenue")]
        public double MonthlyGrossRevenue { get; set; }
    }
}