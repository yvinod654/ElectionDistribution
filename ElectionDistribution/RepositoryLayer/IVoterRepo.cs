using ElectionDistribution.CommanLayer.Model;

namespace ElectionDistribution.RepositoryLayer
{
    public interface IVoterRepo
    {
        public Task<ResponseMessage> AddVoterDetail(VoterDetail request);
        public Task<VoterDetailResponse> GetVoterDetail(int No_Of_Record, string createdby, int SubDivisionId);
    }
}
