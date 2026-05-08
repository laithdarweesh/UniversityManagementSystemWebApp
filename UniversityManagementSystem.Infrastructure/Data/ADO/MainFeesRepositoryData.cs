using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class MainFeesRepositoryData:IMainFeesRepository
    {
        private readonly string _ConnectionString;
        private IAppLog _AppLog;
        public MainFeesRepositoryData(IConfiguration Configuration, IAppLog AppLog)
        {
            _ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _AppLog = AppLog;
        }
        public int Add(MainFees MainFees)
        {
            int MainFeesID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewMainFees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", MainFees.Title);
                        command.Parameters.AddWithValue("@Fees", MainFees.Fees);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedId))
                        {
                            MainFeesID = InsertedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred while adding a new main fees. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return MainFeesID;
        }
        public bool Update(MainFees MainFees)
        {
            int RowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateMainFees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MainFeesID", MainFees.MainFeesID);
                        command.Parameters.AddWithValue("@Title", MainFees.Title);
                        command.Parameters.AddWithValue("@Fees", MainFees.Fees);

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }

                if (RowsAffected > 0)
                {
                    _AppLog.LogInfo($"The record for Fees ID: {MainFees.MainFeesID} was updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in Update method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return (RowsAffected > 0);
        }
        public bool Delete(int FeeId)
        {
            int RowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteFee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FeeID", FeeId);

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }

                if (RowsAffected > 0)
                {
                    _AppLog.LogInfo($"Fee with ID: {FeeId} deleted successfully");
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in Delete method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return (RowsAffected > 0);
        }
        public MainFees GetFeeInfoById(int FeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetMainFeesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MainFeesID", FeeId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MainFees(
                                    reader.GetInt32(reader.GetOrdinal("MainFeesID")),
                                    reader.GetString(reader.GetOrdinal("Title")),
                                    reader.GetDecimal(reader.GetOrdinal("Fees"))
                                    );
                            }
                            else
                                return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetFeeInfoById method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<MainFees> GetAllFees()
        {
            var Fees = new List<MainFees>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllMainFees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Fees.Add(new MainFees(
                                        reader.GetInt32(reader.GetOrdinal("MainFeesID")),
                                        reader.GetString(reader.GetOrdinal("Title")),
                                        reader.GetDecimal(reader.GetOrdinal("Fees"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetAllFees method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
            }

            return Fees;
        }
    }
}