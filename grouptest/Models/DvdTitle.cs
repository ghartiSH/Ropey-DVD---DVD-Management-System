using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grouptest.Models
{
    public class DvdTitle
    {
        [Key]
        public int DvdNumber { get; set; }

        public string? DvdTitleName { get; set; }

        [ForeignKey ("Studio")]
        [Required]
        public int StudioNumber { get; set; } 
        public Studio? Studio { get; set; }

        [NotMapped]
        public string? StudioName { get; set; }

        [ForeignKey ("Producer")]
        [Required]
        public int ProducerNumber { get; set; }
        public Producer? Producer { get; set; }

        [NotMapped]
        public string? ProducerName { get; set; }
        
        [ForeignKey ("DvdCategory")]
        [Required]
        public int CategoryNumber { get; set; }
        public DvdCategory? DvdCategory { get; set; }

        [NotMapped]
        public string? CategoryName { get; set; }

        public DateTime DateReleased { get; set; }
        public int StandardCharge { get; set; }
        public int PenaltyCharge { get; set; }

           
    }
}
