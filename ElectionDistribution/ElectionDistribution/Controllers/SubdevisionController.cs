using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionDistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdevisionController : ControllerBase
    {
        public readonly IRevenueSubDivisionSL _revenueSubDivisionSL;
        public SubdevisionController(IRevenueSubDivisionSL revenueSubDivisionSL)
        {
            _revenueSubDivisionSL = revenueSubDivisionSL;
        }
        [HttpPost("AddSubdevision")]
        public async Task<IActionResult> AddSubdevision(RevenueSubDivision revenueSubDivision)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _revenueSubDivisionSL.AddRevenueSubDivision(revenueSubDivision);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
                return BadRequest(response);
            }
            
        }

        [HttpGet("GetSubdevision")]
        public async Task<IActionResult> GetSubdevision(int SubDivision=0)
        {
            RevenueSubDivisionResponse response = new RevenueSubDivisionResponse();
            try
            {
                response = await _revenueSubDivisionSL.GetRevenueSubDivision(SubDivision);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseMessage.isSuccess = false;
                response.ResponseMessage.message = ex.Message;
                return BadRequest(response);
            }
            
        }
    }
}
