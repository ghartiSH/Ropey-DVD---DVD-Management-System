using System.ComponentModel.DataAnnotations;

namespace grouptest.Models
{
    public class DvdCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        public string? CategoryDescription { get; set; }
        public int AgeRestricted { get; set; }
    }
}
