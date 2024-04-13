using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionDistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        public readonly IVoterSL _voterSL;
        public VoterController(IVoterSL voterSL)
        {
            _voterSL = voterSL;
        }

        [HttpPost("AddVoter")]
        public async Task<IActionResult> AddVoterDetail(VoterDetail voterDetail)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _voterSL.AddVoterDetail(voterDetail);
                return Ok(new { Data = response });
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
                return BadRequest(new { Data = response });
            }
          
        }

        [HttpGet("GetVoterDetail")]
        public async Task<IActionResult> GetVoterDetail(int No_Of_Record,string createdby,int SubDivisionId)
        {
            VoterDetailResponse response=new VoterDetailResponse();
            try
            {
                response = await _voterSL.GetVoterDetail(No_Of_Record, createdby, Convert.ToInt32(SubDivisionId));
                return Ok(response.Details);
            }
            catch (Exception ex)
            {
                response.ResponseMessage.message = ex.Message;
                response.ResponseMessage.isSuccess = false;
                return BadRequest(new { Data = response.ResponseMessage });
            }
            
        }
    }
}
