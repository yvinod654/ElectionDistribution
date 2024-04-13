using ElectionDistribution.CommanLayer.Model;
using System.Threading.Tasks;

namespace ElectionDistribution.ServiceLayer
{
    public interface IUserRegistrationSL
    {
        public Task<ResponseMessage> RegisterUser(UserRegistrationRequest request);
        public Task<ResponseMessage> UserLogin(string UserName, string UserPasssword, int Utype);
        public Task<UserRegistrationResponse> UserDetailByDivisionId(string UserName, string UserPasssword, int Utype, int SubdivisionId);
    }
}
