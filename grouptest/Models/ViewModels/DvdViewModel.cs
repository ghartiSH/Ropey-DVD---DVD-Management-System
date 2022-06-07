namespace grouptest.Models.ViewModels
{
    public class DvdViewModel
    {
        public int Id { get; set; }
        public int ActorNumber { get; set; }
        public string? ActorSurname { get; set; }
        public int DvdNumber { get; set; }
        public string? DvdTitleName { get; set; }
        public DateTime DateReleased { get; set; }
        public int NoOfCopies { get; set; }
        public int StudioNumber { get; set; }
        public int ProducerNumber { get; set; }
        public string? ProducerName { get; set; }
        public string? StudioName { get; set; }

    }
}
