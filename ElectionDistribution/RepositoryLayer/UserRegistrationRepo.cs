using AutoMapper;
using ElectionDistribution.CommanLayer.Model;
using ElectionDistribution.Common_Utility;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
namespace ElectionDistribution.RepositoryLayer
{
    public class UserRegistrationRepo : IUserRegistrationRepo
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        private readonly IMapper _mapper;
        public UserRegistrationRepo(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:myElection"]);
            _mapper = mapper;
        }

        public async Task<ResponseMessage> RegisterUser(UserRegistrationRequest request)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.UserRegistration, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@User_Name", request.UserName);
                    command.Parameters.AddWithValue(parameterName: "@Full_Name", request.Name);
                    command.Parameters.AddWithValue(parameterName: "@User_Email", request.Email);
                    command.Parameters.AddWithValue(parameterName: "@User_Password", request.Password);
                    command.Parameters.AddWithValue(parameterName: "@User_Type", request.UserType);
                    command.Parameters.AddWithValue(parameterName: "@Subdivision_Id", request.SubdevisionId);
                    int status = await command.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        responseMessage.isSuccess = false;
                        responseMessage.message = "Query Not Excuted";
                        return responseMessage;
                    }
                    else
                    {
                        responseMessage.isSuccess = true;
                        responseMessage.message = "Registration successfully";
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
        public async Task<ResponseMessage> UserLogin(string UserName, string UserPasssword, int Utype)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand command = new MySqlCommand(SqlQueries.UserLogin, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@User_Name", UserName);
                    command.Parameters.AddWithValue(parameterName: "@U_Pass", UserPasssword);
                    command.Parameters.AddWithValue(parameterName: "@User_Type", Utype);
                    
                    using (MySqlDataReader dataReader = await command.ExecuteReaderAsync())
                        if (dataReader.HasRows)
                        {
                            responseMessage.isSuccess = true;
                            responseMessage.message = "Valid User";
                            return responseMessage;

                        }
                        else
                        {
                            responseMessage.isSuccess = false;
                            responseMessage.message = "Invalid UserName/Password";
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
        public async Task<UserRegistrationResponse> UserDetailByDivisionId(string UserName, string UserPasssword, int Utype, int SubdivisionId)
        {
            UserRegistrationResponse response = new UserRegistrationResponse();
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
                    command.Parameters.AddWithValue(parameterName: "@SubDivision_ID", SubdivisionId);
                    using (MySqlDataReader dataReader = await command.ExecuteReaderAsync())
                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                UserDetails userDetailsResponse = new UserDetails();
                                userDetailsResponse.Name = dataReader[name: "Name"] != DBNull.Value ? Convert.ToString(dataReader[name: "Name"]) : string.Empty;
                                userDetailsResponse.UserName = dataReader[name: "UserName"] != DBNull.Value ? Convert.ToString(dataReader[name: "UserName"]) : string.Empty;
                                userDetailsResponse.Email = dataReader[name: "Email"] != DBNull.Value ? Convert.ToString(dataReader[name: "Email"]) : string.Empty;
                                userDetailsResponse.SubDivisionName = dataReader[name: "SubDivisionName"] != DBNull.Value ? Convert.ToString(dataReader[name: "SubDivisionName"]) : string.Empty;
                                userDetailsResponse.SubdevisionId = dataReader[name: "RevenueSubDivisionID"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "RevenueSubDivisionID"]) : 0;
                                //userDetailsResponse.MobileNo = dataReader[name: "MobileNo"] != DBNull.Value ? Convert.ToInt32(dataReader[name: "MobileNo"]) : 0;
                                userDetailsResponse.UserType =((UserType)Convert.ToInt32(dataReader[name: "UserType"])).ToString();

                                response.userDetails = response.userDetails?? new List<UserDetails>();
                                response.userDetails.Add(userDetailsResponse);
                            }
                        }
                        else
                        {
                            response.responseMessage = new ResponseMessage() { message = "No Record Found", isSuccess = false  };
                           
                        }
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.responseMessage = new ResponseMessage() { message = ex.Message, isSuccess = false };

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
