namespace grouptest.Models.ViewModels
{
    public class LoanModelView
    {
        public int Id { get; set; }
        public int LoanNumber { get; set; }
        public int CopyNumber { get; set; }
        public int MemberNumber { get; set; }
        public string? MemberLastname { get; set; }
        public string? MemberFirstname { get; set; }
        public string? MemberAddress { get; set; }
        public string? DvdTitleName { get; set; }
        public DateTime? DateOut { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateReturn { get; set; }
        public int NoOfDays { get; set; }
        public int TotalLoans { get; set; }

        public int MemberShipID { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }
    }
}
