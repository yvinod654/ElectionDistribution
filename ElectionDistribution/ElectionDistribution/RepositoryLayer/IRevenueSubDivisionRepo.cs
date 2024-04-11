using ElectionDistribution.CommanLayer.Model;

namespace ElectionDistribution.RepositoryLayer
{
    public interface IRevenueSubDivisionRepo
    {
        public Task<ResponseMessage> AddRevenueSubDivision(RevenueSubDivision subdivision);
        public Task<RevenueSubDivisionResponse> GetRevenueSubDivision(int SubdivisionId);
    }
}
