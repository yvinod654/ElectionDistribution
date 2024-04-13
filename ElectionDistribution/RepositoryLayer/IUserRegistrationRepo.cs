using ElectionDistribution.CommanLayer.Model;
using System.Threading.Tasks;

namespace ElectionDistribution.RepositoryLayer
{
    public interface IUserRegistrationRepo
    {
        public Task<ResponseMessage> RegisterUser(UserRegistrationRequest request);
        public Task<ResponseMessage> UserLogin(string UserName, string UserPasssword, int Utype);
        public Task<UserRegistrationResponse> UserDetailByDivisionId(string UserName, string UserPasssword, int Utype,int SubdivisionId);

    }
}
