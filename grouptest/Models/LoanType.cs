using System.ComponentModel.DataAnnotations;

namespace grouptest.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public string? LoanTypee { get; set; }
        public int LoanDuration  { get; set; } 

    }
}
