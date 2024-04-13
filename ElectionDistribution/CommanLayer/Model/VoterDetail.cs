namespace ElectionDistribution.CommanLayer.Model
{
    public class VoterDetail
    {
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string GuardianName { get; set; }
        public string GuardianMobile { get; set; }
        public int ReceiptCount { get; set; }
        public string Receipts { get; set; }
        public UserRegistrationRequest AddedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SubdevisionId { get; set; }
    }
    public class VoterDetailResponse
    {
        public List<VoterDetail> Details { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
    public class VoterList
    {
        public int No_Of_Record { get; set; }
        public string createdby { get; set; }
        public int SubDivisionId { get; set; }
    }
}
