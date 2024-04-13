using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.RepositoryLayer;

namespace ElectionDistribution.ServiceLayer
{
    public class UserRegistrationSL : IUserRegistrationSL
    {
        public readonly IUserRegistrationRepo _userRegistrationRepo;
        public UserRegistrationSL(IUserRegistrationRepo userRegistrationRepo)
        {
            _userRegistrationRepo = userRegistrationRepo;
        }
        public async Task<ResponseMessage> RegisterUser(UserRegistrationRequest request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                responseMessage = await _userRegistrationRepo.RegisterUser(request);
            }
            catch (Exception ex)
            {
                responseMessage.isSuccess = false;
                responseMessage.message = ex.Message;
            }
            return responseMessage;
        }
        public async Task<ResponseMessage> UserLogin(string UserName,string userPassword,int Utype)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                responseMessage = await _userRegistrationRepo.UserLogin(UserName,userPassword, Utype);
            }
            catch (Exception ex)
            {
                responseMessage.isSuccess = false;
                responseMessage.message = ex.Message;
            }
            return responseMessage;
        }
        public async Task<List<UserRegistrationResponse>> UserDetailByDivisionId(string UserName, string UserPasssword, int Utype, int SubdivisionId)
        {
            List<UserRegistrationResponse> responseMessage ;
            try
            {
                responseMessage = await _userRegistrationRepo.UserDetailByDivisionId(UserName, UserPasssword, Utype, SubdivisionId);
            }
            catch (Exception ex)
            {
                responseMessage = new List<UserRegistrationResponse>() { new UserRegistrationResponse() { responseMessage = new ResponseMessage() { message = ex.Message, isSuccess = false } } };
            }
            return responseMessage;
        }
    }
}
