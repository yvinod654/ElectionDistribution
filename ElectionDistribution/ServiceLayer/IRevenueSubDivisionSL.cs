using ElectionDistribution.CommanLayer.Model;

namespace ElectionDistribution.ServiceLayer
{
    public interface IRevenueSubDivisionSL
    {
        public Task<ResponseMessage> AddRevenueSubDivision(RevenueSubDivision subdivision);
        public Task<RevenueSubDivisionResponse> GetRevenueSubDivision(int SubdivisionId);
    }
}
