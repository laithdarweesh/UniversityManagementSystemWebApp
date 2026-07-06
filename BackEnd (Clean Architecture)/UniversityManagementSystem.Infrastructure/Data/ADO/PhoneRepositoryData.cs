using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class PhoneRepositoryData : IPhoneRepository
    {
        private readonly struct PhoneOrdinals
        {
            public readonly int PhoneId;
            public readonly int PhoneNumber;
            public readonly int PersonId;
            public PhoneOrdinals(SqlDataReader reader)
            {
                PhoneId = reader.GetOrdinal("PhoneId");
                PhoneNumber = reader.GetOrdinal("PhoneNumber");
                PersonId = reader.GetOrdinal("PersonId");
            }
        }

        private readonly string _connectionString;
        private readonly IAppLog _appLog;
        public PhoneRepositoryData(IConfiguration configuration, IAppLog appLog)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Missing connection string");

            _appLog = appLog;
        }
        private static Phone MapPhone(SqlDataReader reader, in PhoneOrdinals ordinals)
        {
            return Phone.Load(
                reader.GetInt32(ordinals.PhoneId),
                reader.GetString(ordinals.PhoneNumber),
                reader.GetInt32(ordinals.PersonId)
                );
        }
        public int Add(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException(nameof(phone));

            int phoneId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_Add", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = phone.PhoneNumber;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = phone.PersonId;

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            phoneId = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new OperationFailedException("Stored procedure returned a null or invalid result.");
                        }
                    }
                }

                if (phoneId > 0)
                    _appLog.LogInfo($"New phone added to system with Id: {phoneId}");
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred while adding a new Phone Number. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return phoneId;
        }
        public void Update(Phone phone)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PhoneId", SqlDbType.Int).Value = phone.PhoneId;
                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = phone.PhoneNumber;

                        command.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                        connection.Open();
                        command.ExecuteNonQuery();

                        object returnedValue = command.Parameters["@Result"].Value;

                        // The stored procedure should always assign @Result. If not, treat it as an unexpected failure.

                        if (returnedValue == DBNull.Value)
                            throw new OperationFailedException("Output parameter @Result was not assigned.");

                        result = (int)returnedValue;
                    }
                }

                if (result > 0)
                {
                    _appLog.LogInfo($"The record for Phone Id: {phone.PhoneId} was updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in UpdatePhoneNumber method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Phone? GetById(int phoneId)
        {
            if (phoneId <= 0)
                throw new ArgumentException("phoneId must be greater than zero.", nameof(phoneId));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_GetById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PhoneId", SqlDbType.Int).Value = phoneId;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new PhoneOrdinals(reader);

                            return MapPhone(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in Get method by Phone ID. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Phone? GetByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("phoneNumber can't be empty", nameof(phoneNumber));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_GetByPhoneNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = phoneNumber;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new PhoneOrdinals(reader);

                            return MapPhone(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in Get method by Phone Number. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Phone> GetAll()
        {
            var phones = new List<Phone>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return phones;

                            // Cache all ordinals once

                            var ordinals = new PhoneOrdinals(reader);

                            while (reader.Read())
                            {
                                phones.Add(MapPhone(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAllPhones method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return phones;
        }
        public List<Phone> GetByPersonId(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonId must be greater than zero.", nameof(personId));

            var phonesByPerson = new List<Phone>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Phone_GetByPersonId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return phonesByPerson;

                            // Cache all ordinals once

                            var ordinals = new PhoneOrdinals(reader);

                            while (reader.Read())
                            {
                                phonesByPerson.Add(MapPhone(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAllPhonesByPerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return phonesByPerson;
        }
    }
}