using System.ComponentModel.DataAnnotations;

namespace grouptest.Models
{
    public class Studio
    {
        [Key]
        public int StudioNumber { get; set; }
        public string? StudioName { get; set; }
    }
}
