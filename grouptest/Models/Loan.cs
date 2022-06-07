using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grouptest.Models
{
    public class Loan
    {
        [Key]
        public int LoanNumber { get; set; }

        [ForeignKey ("LoanType")]
        [Required]
        public int LoanTypeNumber { get; set; }
        public LoanType? LoanType { get; set; }

        [NotMapped]
        public string? LoanTypee { get; set; }

        [ForeignKey ("Member")]
        [Required]
        public int MemberNumber { get; set; }
        public Member? Member { get; set; }

        [NotMapped]
        public string? MemberLastname { get; set; }

        [ForeignKey ("DvdCopy")]
        [Required]
        public int CopyNumber { get; set; }
        public DvdCopy? DvdCopy { get; set; }

        public DateTime DateOut { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateReturn { get; set; }


    }
}
