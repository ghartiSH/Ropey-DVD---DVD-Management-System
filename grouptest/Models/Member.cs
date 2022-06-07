using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grouptest.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }

        public string? MemberLastname { get; set; }

        public string? MemberFirstname { get; set; }

        public string? MemberAddress { get; set; }

        public DateTime MemberDob { get; set; }
        
        [ForeignKey ("MembershipCategory")]
        [Required]
        public int MembershipID { get; set; }
        public MembershipCategory? MembershipCategory { get; set; }

        [NotMapped]
        public string? MembershipName { get; set; }

    }
}
