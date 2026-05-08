using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Shared.Utilities;
using UniversityManagementSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class CountryRepositoryData:ICountryRepository
    {
        private readonly string _ConnectionString;
        private readonly IAppLog _AppLog;
        public CountryRepositoryData(IConfiguration Configuration, IAppLog AppLog)
        {
            _ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _AppLog = AppLog;
        }
        public Country GetCountry(int CountryID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryID", CountryID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Country(
                                    reader.GetInt32(reader.GetOrdinal(("CountryID"))),
                                    reader.GetString(reader.GetOrdinal("CountryName"))
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
                _AppLog.LogError($"Error occurred in GetCountryInfoByID method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Country GetCountry(string CountryName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Country(
                                    reader.GetInt32(reader.GetOrdinal("CountryID")),
                                    reader.GetString(reader.GetOrdinal("CountryName"))
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
                _AppLog.LogError($"Error occurred in GetCountryInfoByName method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Country> GetAllCountries()
        {
            var Countries = new List<Country>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Countries.Add(
                                    new Country(
                                        reader.GetInt32(reader.GetOrdinal("CountryID")),
                                        reader.GetString(reader.GetOrdinal("CountryName"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetAllCountries method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return Countries;
        }
    }
}