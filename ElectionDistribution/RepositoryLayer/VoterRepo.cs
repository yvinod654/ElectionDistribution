using AutoMapper;
using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.Common_Utility;
using MySqlConnector;

namespace ElectionDistribution.RepositoryLayer
{
    public class VoterRepo : IVoterRepo
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        private readonly IMapper _mapper;
        public VoterRepo(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:myElection"]);
            _mapper = mapper;
        }
        public async Task<ResponseMessage> AddVoterDetail(VoterDetail request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.AddVoter, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@Village_Name", request.VillageName);
                    command.Parameters.AddWithValue(parameterName: "@Guardian_Name", request.GuardianName);
                    command.Parameters.AddWithValue(parameterName: "@Guardian_Mobile", request.GuardianMobile);
                    command.Parameters.AddWithValue(parameterName: "@Receipt_Count", request.ReceiptCount);
                    command.Parameters.AddWithValue(parameterName: "@Receipts_No", request.Receipts);
                    command.Parameters.AddWithValue(parameterName: "@AddedByUser", request.AddedByUser);
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
        public async Task<VoterDetailResponse> GetVoterDetail(int No_Of_Record, string createdby, int SubDivisionId)
        {
            VoterDetailResponse response = new VoterDetailResponse();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.UserBySubdivision, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@No_Of_Record", No_Of_Record);
                    command.Parameters.AddWithValue(parameterName: "@createdby", createdby);
                    command.Parameters.AddWithValue(parameterName: "@SubDivisionId", SubDivisionId);
                    using (MySqlDataReader dataReader = await command.ExecuteReaderAsync())
                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                VoterDetail voterDetailsResponse = new VoterDetail();
                                voterDetailsResponse.VillageName = dataReader[name: "VillageName"] != DBNull.Value ? Convert.ToString(dataReader[name: "VillageName"]) : string.Empty;
                                voterDetailsResponse.GuardianName = dataReader[name: "GuardianName"] != DBNull.Value ? Convert.ToString(dataReader[name: "GuardianName"]) : string.Empty;
                                voterDetailsResponse.GuardianMobile = dataReader[name: "GuardianMobile"] != DBNull.Value ? Convert.ToInt64(dataReader[name: "GuardianMobile"]) : 0;
                                voterDetailsResponse.ReceiptCount = dataReader[name: "ReceiptCount"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "ReceiptCount"]) : 0;
                                voterDetailsResponse.Receipts = dataReader[name: "Receipts"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "Receipts"]) : 0;
                                voterDetailsResponse.AddedByUser = dataReader[name: "AddedByUser"]!=DBNull.Value ? Convert.ToString(dataReader[name: "AddedByUser"]):string.Empty;
                                voterDetailsResponse.CreatedDate = Convert.ToDateTime(dataReader[name: "CreatedDate"]);
                                response.Details.Add(voterDetailsResponse);
                            }
                        }
                        else
                        {
                            response.ResponseMessage.isSuccess = false;
                            response.ResponseMessage.message = "No Record Found";

                        }
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.ResponseMessage.isSuccess = false;
                response.ResponseMessage.message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }
    }
}

