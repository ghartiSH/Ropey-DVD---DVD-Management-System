namespace grouptest.Models.ViewModels
{
    public class ChargeViewModel
    {
        public int Id { get; set; }
        public int TotalCharge { get; set; }   
        public DateTime? DateDue { get; set; }
        public DateTime? DateReturn { get; set; }
        public int StandardCharge { get; set; }
        public int PenaltyCharge { get; set; }
        public string? Message { get; set; }

        public int CopyNumber { get; set; }
        public string? DvdTitleName { get; set; }
    }
}
