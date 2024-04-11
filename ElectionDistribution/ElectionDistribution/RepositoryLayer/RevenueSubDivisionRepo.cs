using AutoMapper;
using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.Common_Utility;
using MySqlConnector;

namespace ElectionDistribution.RepositoryLayer
{
    public class RevenueSubDivisionRepo : IRevenueSubDivisionRepo
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        private readonly IMapper _mapper;
        public RevenueSubDivisionRepo(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:myElection"]);
            _mapper = mapper;
        }

        public async Task<ResponseMessage> AddRevenueSubDivision(RevenueSubDivision request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.SubdivisionRegistration, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@SubDivision_Name", request.SubDivisionName);
                    command.Parameters.AddWithValue(parameterName: "@SDM_Name", request.SDMName);
                    command.Parameters.AddWithValue(parameterName: "@SDM_Mobile", request.SDMMobile);
                    command.Parameters.AddWithValue(parameterName: "@Total_Receipt", request.TotalReceipt);
                    command.Parameters.AddWithValue(parameterName: "@Distict_Name", request.DistictName);
                    int status = await command.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        responseMessage.isSuccess = false;
                        responseMessage.message = "No Data save";
                        return responseMessage;
                    }
                    else
                    {
                        responseMessage.isSuccess = true;
                        responseMessage.message = "Data save successfully";
                        return responseMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                responseMessage.isSuccess = false;
                responseMessage.message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return responseMessage;
        }
        public async Task<RevenueSubDivisionResponse> GetRevenueSubDivision(int SubdivisionId)
        {
            RevenueSubDivisionResponse revenueSubDivision = new RevenueSubDivisionResponse();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.GetSubdivision, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@DivisionId", SubdivisionId);
                    using (MySqlDataReader dataReader = await command.ExecuteReaderAsync())
                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                RevenueSubDivision revenueSub = new RevenueSubDivision();
                                revenueSub.SubDivisionName = dataReader[name: "SubDivisionName"] != DBNull.Value ? Convert.ToString(dataReader[name: "SubDivisionName"]) : string.Empty;
                                revenueSub.SDMName = dataReader[name: "SDMName"] != DBNull.Value ? Convert.ToString(dataReader[name: "SDMName"]) : string.Empty;
                                revenueSub.SDMMobile = dataReader[name: "SDMMobile"] != DBNull.Value ? Convert.ToInt64(dataReader[name: "SDMMobile"]) : 0;
                                revenueSub.TotalReceipt = dataReader[name: "TotalReceipt"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "TotalReceipt"]) : 0;
                                revenueSub.DistictName = dataReader[name: "DistictName"] != DBNull.Value ? Convert.ToString(dataReader[name: "DistictName"]) : string.Empty;
                                revenueSub.Id = dataReader[name: "ID"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "ID"]) : 0;
                                revenueSubDivision.revenueSubDivisions.Add(revenueSub);
                            }
                        }
                        else
                        {
                            revenueSubDivision.ResponseMessage.isSuccess = true;
                            revenueSubDivision.ResponseMessage.message = "No Record Found";
                           
                        }
                }

            }
            catch (Exception ex)
            {
                revenueSubDivision.ResponseMessage.isSuccess = false;
                revenueSubDivision.ResponseMessage.message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return revenueSubDivision;
        }
    }
}
