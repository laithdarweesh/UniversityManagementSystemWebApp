using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class PersonRepositoryData
    {
        private readonly string _ConnectionString;
        private readonly IAppLog _AppLog;
        public PersonRepositoryData(IConfiguration Configuration, IAppLog AppLog)
        {
            _ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _AppLog = AppLog;
        }
        public int Add(Person Person)
        {
            int PersonId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", Person.FirstName);

                        command.Parameters.AddWithValue("@SecondName", clsGlobal.ToDbNull(Person.SecondName));
                        command.Parameters.AddWithValue("@ThirdName", clsGlobal.ToDbNull(Person.ThirdName));

                        command.Parameters.AddWithValue("@LastName", Person.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Person.Gendor);

                        command.Parameters.AddWithValue("@Email", clsGlobal.ToDbNull(Person.Email));
                        command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryId);
                        command.Parameters.AddWithValue("@ImagePath", clsGlobal.ToDbNull(Person.ImagePath));

                        command.Parameters.AddWithValue("@CreatedDate", Person.CreatedDate);
                        command.Parameters.AddWithValue("@LastStatusDate", Person.LastStatusDate);
                        command.Parameters.AddWithValue("@CreatedByAdminID", Person.CreatedByAdminId);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InserteId))
                        {
                            PersonId = InserteId;

                            switch (PersonId)
                            {
                                case -2:
                                    throw new Exception("National Number already exists.");

                                case -3:
                                    throw new Exception("Invalid DateOfBirth.");

                                default:
                                    if (PersonId <= 0)
                                        throw new Exception(@"Unexpected negative return value from 
                                                                  the stored procedure.");
                                    break;
                            }
                        }
                        else
                        {
                            throw new Exception(@"Unexpected null or invalid return value from the 
                                                      stored procedure.");
                        }
                    }
                }

                if (PersonId > 0)
                    _AppLog.LogInfo($"New Person added to System with ID: {PersonId}");
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred while adding a new person. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
            }

            return PersonId;
        }
        public bool Update(Person Person)
        {
            int Result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", Person.PersonId);
                        command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", Person.FirstName);

                        command.Parameters.AddWithValue("@SecondName", clsGlobal.ToDbNull(Person.SecondName));
                        command.Parameters.AddWithValue("@ThirdName", clsGlobal.ToDbNull(Person.ThirdName));

                        command.Parameters.AddWithValue("@LastName", Person.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Person.Gendor);

                        command.Parameters.AddWithValue("@Email", clsGlobal.ToDbNull(Person.Email));
                        command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryId);

                        command.Parameters.AddWithValue("@ImagePath", clsGlobal.ToDbNull(Person.ImagePath));

                        command.Parameters.AddWithValue("@CreatedDate", Person.CreatedDate);
                        command.Parameters.AddWithValue("@LastStatusDate", Person.LastStatusDate);
                        command.Parameters.AddWithValue("@CreatedByAdminID", Person.CreatedByAdminId);

                        connection.Open();
                        Result = (int)command.ExecuteScalar();
                    }
                }

                if (Result == 1)
                {
                    _AppLog.LogInfo($"The record for Person ID: {Person.PersonId} was updated successfully.");

                    return true;
                }

                if (Result == 0)
                    throw new Exception("PersonID not found.");

                else if (Result == -1)
                    throw new Exception("An error occurred while updating the person info.");

                else
                    throw new Exception($"Unknown result from the stored procedure.{Result}");
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in UpdatePerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");

                return false;
            }
        }
        public bool Delete(int PersonId)
        {
            int RowsAffected = 0, result = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonId);

                        SqlParameter returnvalue = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        command.Parameters.Add(returnvalue);

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                        result = (int)returnvalue.Value;
                    }
                }

                if (result == -1)
                    throw new Exception($"Person with ID {PersonId} does not exist.");

                else if (result == 0)
                    throw new Exception($"An error occurred while deleting Person with ID: {PersonId}.");

                else
                    throw new Exception($"Unknown result from the stored procedure.{result}");
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in DeletePerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return (RowsAffected > 0);
        }
        public Person GetByPersonId(int PersonId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPersonInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonId", PersonId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Person(
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("NationalNo")),
                                    reader.GetString(reader.GetOrdinal("FirstName")),

                                    reader.IsDBNull(reader.GetOrdinal("SecondName")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("SecondName")),

                                    reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("ThirdName")),

                                    reader.GetString(reader.GetOrdinal("LastName")),
                                    reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    reader.GetByte(reader.GetOrdinal("Gendor")),

                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("Email")),

                                    reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),

                                    reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("ImagePath")),

                                    reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByAdminID"))
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
                _AppLog.LogError($"Error occurred in GetPersonInfoByID method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Person GetByNationalNo(string NationalNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPersonByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Person(
                                        reader.GetInt32(reader.GetOrdinal("PersonID")),
                                        reader.GetString(reader.GetOrdinal("NationalNo")),
                                        reader.GetString(reader.GetOrdinal("FirstName")),

                                        reader.IsDBNull(reader.GetOrdinal("SecondName")) ? (string?)null :
                                        reader.GetString(reader.GetOrdinal("SecondName")),

                                        reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? (string?)null :
                                        reader.GetString(reader.GetOrdinal("ThirdName")),

                                        reader.GetString(reader.GetOrdinal("LastName")),
                                        reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                        reader.GetByte(reader.GetOrdinal("Gendor")),

                                        reader.IsDBNull(reader.GetOrdinal("Email")) ? (string?)null :
                                        reader.GetString(reader.GetOrdinal("Email")),

                                        reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),

                                        reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? (string?)null :
                                        reader.GetString(reader.GetOrdinal("ImagePath")),

                                        reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                        reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                                        reader.GetInt32(reader.GetOrdinal("CreatedByAdminID"))
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
                _AppLog.LogError($"Error occurred in GetPersonInfoByNationalNo method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Person> GetAllPeople()
        {
            var AllPeople = new List<Person>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AllPeople.Add(
                                    new Person(
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("NationalNo")),
                                    reader.GetString(reader.GetOrdinal("FirstName")),

                                    reader.IsDBNull(reader.GetOrdinal("SecondName")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("SecondName")),

                                    reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("ThirdName")),

                                    reader.GetString(reader.GetOrdinal("LastName")),
                                    reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    reader.GetByte(reader.GetOrdinal("Gendor")),

                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("Email")),

                                    reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),

                                    reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? (string?)null :
                                    reader.GetString(reader.GetOrdinal("ImagePath")),

                                    reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByAdminID"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in GetAllPeople method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return AllPeople;
        }
        public bool IsPersonExist(int PersonId)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPersonExistByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonId);
                        connection.Open();

                        int result = (int)command.ExecuteScalar();
                        IsFound = (result == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in IsPersonExist method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return IsFound;
        }
        public bool IsPersonExist(string NationalNo)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPersonExistByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        connection.Open();

                        int result = (int)command.ExecuteScalar();
                        IsFound = (result == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                _AppLog.LogError($"Error occurred in IsPersonExist method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return IsFound;
        }
    }
}