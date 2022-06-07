using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grouptest.Models
{
    public class CastMember
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey ("Actor")]
        [Required]
        public int ActorNumber { get; set; }
        public Actor? Actor { get; set; }

        [NotMapped]
        public string? ActorSurname { get; set; }

        [ForeignKey ("DvdTitle")]
        [Required]
        public int DvdNumber { get; set; }
        public DvdTitle? DvdTitle { get; set; }
        [NotMapped]
        public string? DvdTitleName { get; set; }

    }
}
