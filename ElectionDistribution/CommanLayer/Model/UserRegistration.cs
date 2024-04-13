namespace ElectionDistribution.CommanLayer.Model
{
    public class UserRegistrationRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public int RevenueSubDivisionID { get; set; }
    }
    public class UserRegistrationResponse
    {
        public List<UserDetails> userDetails { get; set; }    
        public ResponseMessage responseMessage { get; set; }
    }
    public class UserDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SubDivisionName { get; set; }
        public string UserType { get; set; }
        public long MobileNo { get; set; }
        public int? SubdevisionId { get; set; }

    }
    public class ResponseMessage
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }
}
