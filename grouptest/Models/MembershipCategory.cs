using System.ComponentModel.DataAnnotations;

namespace grouptest.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }
        public string? MembershipCategoryDescription { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }   

    }
}
