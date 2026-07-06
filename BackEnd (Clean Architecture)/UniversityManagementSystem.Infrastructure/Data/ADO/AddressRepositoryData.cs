using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class AddressRepositoryData : IAddressRepository
    {
        private readonly struct AddressOrdinals
        {
            public readonly int AddressId;
            public readonly int AddressName;
            public readonly int PersonId;
            public AddressOrdinals(SqlDataReader reader)
            {
                AddressId = reader.GetOrdinal("AddressId");
                AddressName = reader.GetOrdinal("Address");
                PersonId = reader.GetOrdinal("PersonId");
            }
        }
        private readonly string _connectionString;
        private readonly IAppLog _appLog;
        public AddressRepositoryData(IConfiguration configuration, IAppLog appLog)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing connection string");

            _appLog = appLog;
        }
        private static Address MapAddress(SqlDataReader reader, in AddressOrdinals ordinals)
        {
            return Address.Load(
                reader.GetInt32(ordinals.AddressId),
                reader.GetString(ordinals.AddressName),
                reader.GetInt32(ordinals.PersonId)
                );
        }
        public int Add(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            int addressId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Address_Add", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = address.AddressName;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = address.PersonId;

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            addressId = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new OperationFailedException("Stored procedure returned a null or invalid result.");
                        }
                    }
                }

                if (addressId > 0)
                    _appLog.LogInfo($"New address added to system with Id: {addressId}");
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred while adding a new address. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return addressId;
        }
        public void Update(Address address)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Address_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@AddressId", SqlDbType.Int).Value = address.AddressId;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = address.AddressName;
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
                    _appLog.LogInfo($"The record for Address Id: {address.AddressId} was updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in UpdateAddress method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Address? GetById(int addressId)
        {
            if (addressId <= 0)
                throw new ArgumentException("AddressId must be greater than zero.", nameof(addressId));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Address_GetById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AddressId", SqlDbType.Int).Value = addressId;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new AddressOrdinals(reader);

                            return MapAddress(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAddressInfoById method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Address> GetByPersonId(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonId must be greater than zero.", nameof(personId));

            var addressesForPerson = new List<Address>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Address_GetByPersonId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return addressesForPerson;

                            // Cache all ordinals once

                            var ordinals = new AddressOrdinals(reader);

                            while (reader.Read())
                            {
                                addressesForPerson.Add(MapAddress(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAddressesByPersonId method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return addressesForPerson;
        }
        public List<Address> GetAll()
        {
            var addresses = new List<Address>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Address_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return addresses;

                            // Cache all ordinals once

                            var ordinals = new AddressOrdinals(reader);

                            while (reader.Read())
                            {
                                addresses.Add(MapAddress(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAllAddresses method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return addresses;
        }
    }
}