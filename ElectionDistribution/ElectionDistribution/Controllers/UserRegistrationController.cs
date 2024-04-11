using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ElectionDistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        public readonly IUserRegistrationSL _userRegistrationSL;
        public UserRegistrationController(IUserRegistrationSL userRegistrationSL)
        {
            _userRegistrationSL = userRegistrationSL;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRegistrationRequest userRegistration)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userRegistrationSL.RegisterUser(userRegistration);
            }
            catch(Exception ex)
            {
                response.isSuccess= false;
                response.message=ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("UserLogin")]
        public async Task<IActionResult> UserLogin(UserRegistrationRequest userRegistration)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userRegistrationSL.UserLogin(userRegistration.UserName,userRegistration.Password,Convert.ToInt32(userRegistration.UserType));
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("GetUserByDivision")]
        public async Task<IActionResult> GetUserByDivision(UserRegistrationRequest userRegistration)
        {
            List<UserRegistrationResponse> response;
            try
            {
                response = await _userRegistrationSL.UserDetailByDivisionId(userRegistration.UserName, userRegistration.Password, Convert.ToInt32(userRegistration.UserType), Convert.ToInt32(userRegistration.RevenueSubDivisionID));
            }
            catch (Exception ex)
            {
                response = new List<UserRegistrationResponse>() { new UserRegistrationResponse() { responseMessage = new ResponseMessage() { message = ex.Message, isSuccess = false } } };
            }
            return Ok(new { Data = response });
        }
    }
}
