using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.RepositoryLayer;

namespace ElectionDistribution.ServiceLayer
{
    public class VoterSL : IVoterSL
    {
        public readonly IVoterRepo _voterRepo;
        public VoterSL(IVoterRepo voterRepo)
        {
            _voterRepo = voterRepo;
        }
        public async Task<ResponseMessage> AddVoterDetail(VoterDetail request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                responseMessage = await _voterRepo.AddVoterDetail(request);
            }
            catch (Exception ex)
            {
                responseMessage.isSuccess = false;
                responseMessage.message = ex.Message;
            }
            return responseMessage;
        }
        public async Task<VoterDetailResponse> GetVoterDetail(int No_Of_Record, string createdby, int SubDivisionId)
        {
            VoterDetailResponse voterDetailResponse=new VoterDetailResponse();
            try
            {
                voterDetailResponse = await _voterRepo.GetVoterDetail(No_Of_Record,createdby,SubDivisionId);
            }
            catch (Exception ex)
            {
                voterDetailResponse.ResponseMessage.isSuccess = false;
                voterDetailResponse.ResponseMessage.message = ex.Message;
            }
            return voterDetailResponse;
        }
    }
}
