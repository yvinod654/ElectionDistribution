namespace ElectionDistribution.CommanLayer.Model
{
    public class RevenueSubDivision
    {
        public int Id { get; set; }
        public string SubDivisionName { get; set; }
        public string SDMName { get; set; }
        public long SDMMobile { get; set; }
        public string DistictName { get; set; }
        public int TotalReceipt { get; set; }
        

    }
    public class RevenueSubDivisionResponse
    {
        public List<RevenueSubDivision> revenueSubDivisions { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
}
