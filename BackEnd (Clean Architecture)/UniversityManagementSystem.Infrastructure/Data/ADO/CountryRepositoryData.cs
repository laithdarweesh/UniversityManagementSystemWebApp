using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class CountryRepositoryData : ICountryRepository
    {
        private readonly struct CountryOrdinals
        {
            public readonly int CountryId;
            public readonly int CountryName;
            public CountryOrdinals(SqlDataReader reader)
            {
                CountryId = reader.GetOrdinal("CountryId");
                CountryName = reader.GetOrdinal("CountryName");
            }
        }
        private readonly string _connectionString;
        private readonly IAppLog _appLog;
        public CountryRepositoryData(IConfiguration configuration, IAppLog appLog)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Missing connection string");

            _appLog = appLog;
        }
        private static Country MapCountry(SqlDataReader reader, in CountryOrdinals ordinals)
        {
            return Country.Load(
                reader.GetInt32(ordinals.CountryId),
                reader.GetString(ordinals.CountryName)
                );
        }
        public Country? GetById(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("CountryId must be greater than zero.", nameof(countryId));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Country_GetById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryId;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new CountryOrdinals(reader);

                            return MapCountry(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetCountryInfoById method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Country? GetByName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentException("CountryName can't be empty", nameof(countryName));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Country_GetByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CountryName", SqlDbType.NVarChar, 100).Value = countryName;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new CountryOrdinals(reader);

                            return MapCountry(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetCountryInfoByName method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Country> GetAll()
        {
            var countries = new List<Country>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Country_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return countries;

                            // Cache all ordinals once

                            var ordinals = new CountryOrdinals(reader);

                            while (reader.Read())
                            {
                                countries.Add(MapCountry(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAllCountries method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return countries;
        }
    }
}