using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grouptest.Models
{
    public class DvdCopy
    {
        [Key]
        public int CopyNumber { get; set; }

        [ForeignKey ("DvdTitle")]
        [Required]
        public int DvdNumber { get; set; }
        public DvdTitle? DvdTitle { get; set; }

        [NotMapped]
        public string? DvdTitleName { get; set; }
        public DateTime DatePurchased { get; set; }


    }
}
