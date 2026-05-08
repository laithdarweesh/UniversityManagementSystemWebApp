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
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Shared.Utilities;
using UniversityManagementSystem.Application.Interfaces.Addresses;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class AddressRepositoryData: IAddressRepository
    {
        private readonly string _ConnectionString;
        private readonly IAppLog _AppLog;
        public AddressRepositoryData(IConfiguration Configuration, IAppLog AppLog) 
        {
            _ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _AppLog = AppLog;
        }
        public int Add(Address Address)
        {
            int AddressId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Address", Address.AddressName);
                        command.Parameters.AddWithValue("@PersonId", Address.PersonId);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedId))
                        {
                            AddressId = InsertedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred while adding a new address. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return AddressId;
        }
        public bool Update(Address Address)
        {
            int RowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AddressID", Address.AddressID);
                        command.Parameters.AddWithValue("@Address", Address.AddressName);

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }

                if (RowsAffected > 0)
                {
                    _AppLog.LogInfo($"The record for Address ID: {Address.AddressID} was updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in UpdateAddress method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return (RowsAffected > 0);
        }
        public Address GetById(int AddressId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAddressByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AddressID", AddressId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Address(
                                    reader.GetInt32(reader.GetOrdinal("AddressID")),
                                    reader.GetString(reader.GetOrdinal("Address")),
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
                _AppLog.LogError($"Error occurred in GetAddressInfoByID method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Address> GetAddressesByPersonID(int PersonId)
        {
            var AddressesForPerson = new List<Address>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAddressByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AddressesForPerson.Add(
                                    new Address(
                                            reader.GetInt32(reader.GetOrdinal("AddressID")),
                                            reader.GetString(reader.GetOrdinal("Address")),
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
                _AppLog.LogError($"Error occurred in GetAddressInfoByPersonID method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return AddressesForPerson;
        }
        public List<Address> GetAllAddresses()
        {
            var Addresses = new List<Address>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllAddresses", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Addresses.Add(
                                    new Address(
                                            reader.GetInt32(reader.GetOrdinal("AddressID")),
                                            reader.GetString(reader.GetOrdinal("Address")),
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
                _AppLog.LogError($"Error occurred in GetAllAddresses method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return Addresses;
        }
    }
}