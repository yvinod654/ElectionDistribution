using AutoMapper;
using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.RepositoryLayer;
using MySqlConnector;

namespace ElectionDistribution.ServiceLayer
{
    public class RevenueSubDivisionSL : IRevenueSubDivisionSL
    {
        public readonly IRevenueSubDivisionRepo _revenueSubDivisionRepo;
        public RevenueSubDivisionSL(IRevenueSubDivisionRepo revenueSubDivisionRepo)
        {
            _revenueSubDivisionRepo = revenueSubDivisionRepo;
        }
        public async Task<ResponseMessage> AddRevenueSubDivision(RevenueSubDivision request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                responseMessage = await _revenueSubDivisionRepo.AddRevenueSubDivision(request);
            }
            catch (Exception ex)
            {
                responseMessage.isSuccess = false;
                responseMessage.message = ex.Message;
            }
            return responseMessage;
        }
        public async Task<RevenueSubDivisionResponse> GetRevenueSubDivision(int SubDivision)
        {
            RevenueSubDivisionResponse revenueSubDivision = new RevenueSubDivisionResponse();
            try
            {
                revenueSubDivision = await _revenueSubDivisionRepo.GetRevenueSubDivision(SubDivision);
            }
            catch (Exception ex)
            {
                revenueSubDivision.ResponseMessage.isSuccess = false;
                revenueSubDivision.ResponseMessage.message = ex.Message;
            }
            return revenueSubDivision;
        }
    }
}
