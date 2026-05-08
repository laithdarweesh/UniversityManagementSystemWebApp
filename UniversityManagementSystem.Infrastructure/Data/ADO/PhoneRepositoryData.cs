using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class PhoneRepositoryData: IPhoneRepository
    {
        private readonly string _ConnectionString;
        private readonly IAppLog _AppLog;
        public PhoneRepositoryData(IConfiguration Configuration, IAppLog AppLog)
        {
            _ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _AppLog = AppLog;
        }
        public int Add(Phone Phone)
        {
            int PhoneID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPhoneNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PhoneNumber", Phone.PhoneNumber);
                        command.Parameters.AddWithValue("@PersonID", Phone.PersonId);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedId))
                        {
                            PhoneID = InsertedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred while adding a new Phone Number. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return PhoneID;
        }
        public bool Update(Phone Phone)
        {
            int RowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePhoneNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PhoneID", Phone.PhoneId);
                        command.Parameters.AddWithValue("@PhoneNumber", Phone.PhoneNumber);

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }

                if (RowsAffected > 0)
                {
                    _AppLog.LogInfo($"The record for Phone ID: {Phone.PhoneId} was updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in UpdatePhoneNumber method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return (RowsAffected > 0);
        }
        public Phone Get(int PhoneId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPhoneInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PhoneID", PhoneId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Phone(
                                    reader.GetInt32(reader.GetOrdinal("PhoneID")),
                                    reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID"))
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
                _AppLog.LogError($"Error occurred in Get method by Phone ID. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Phone Get(string PhoneNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPhoneInfoByPhoneNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Phone(
                                    reader.GetInt32(reader.GetOrdinal("PhoneID")),
                                    reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID"))
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
                _AppLog.LogError($"Error occurred in Get method by Phone Number. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Phone> GetAllPhones()
        {
            var Phones = new List<Phone>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllPhones", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Phones.Add(
                                    new Phone(
                                        reader.GetInt32(reader.GetOrdinal("PhoneID")),
                                        reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                        reader.GetInt32(reader.GetOrdinal("PersonID"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetAllPhones method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return Phones;
        }
        public List<Phone> GetAllPhonesByPerson(int PersonId)
        {
            var PhonesByPerson = new List<Phone>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllPhonesByPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PhonesByPerson.Add(
                                        new Phone(
                                            reader.GetInt32(reader.GetOrdinal("PhoneID")),
                                            reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                            reader.GetInt32(reader.GetOrdinal("PersonID"))
                                    ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetAllPhonesByPerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return PhonesByPerson;
        }
    }
}
