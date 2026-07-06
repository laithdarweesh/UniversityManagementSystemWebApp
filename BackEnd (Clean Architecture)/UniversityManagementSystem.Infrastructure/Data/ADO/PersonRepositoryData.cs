using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data.ADO
{
    public class PersonRepositoryData : IPersonRepository
    {
        private readonly struct PersonOrdinals
        {
            public readonly int PersonId;
            public readonly int NationalNo;
            public readonly int FirstName;
            public readonly int SecondName;
            public readonly int ThirdName;
            public readonly int LastName;
            public readonly int DateOfBirth;
            public readonly int Gendor;
            public readonly int Email;
            public readonly int NationalityCountryId;
            public readonly int ImagePath;
            public readonly int CreatedAt;
            public readonly int UpdatedAt;
            public readonly int CreatedByUserId;
            public readonly int UpdatedByUserId;
            public readonly int IsActive;
            public PersonOrdinals(SqlDataReader reader)
            {
                PersonId = reader.GetOrdinal("PersonId");
                NationalNo = reader.GetOrdinal("NationalNo");

                FirstName = reader.GetOrdinal("FirstName");
                SecondName = reader.GetOrdinal("SecondName");
                ThirdName = reader.GetOrdinal("ThirdName");
                LastName = reader.GetOrdinal("LastName");

                DateOfBirth = reader.GetOrdinal("DateOfBirth");
                Gendor = reader.GetOrdinal("Gendor");
                Email = reader.GetOrdinal("Email");
                NationalityCountryId = reader.GetOrdinal("NationalityCountryId");
                ImagePath = reader.GetOrdinal("ImagePath");
                CreatedAt = reader.GetOrdinal("CreatedAt");
                UpdatedAt = reader.GetOrdinal("UpdatedAt");
                CreatedByUserId = reader.GetOrdinal("CreatedByUserId");
                UpdatedByUserId = reader.GetOrdinal("UpdatedByUserId");
                IsActive = reader.GetOrdinal("IsActive");
            }
        }

        private readonly string _connectionString;
        private readonly IAppLog _appLog;
        public PersonRepositoryData(IConfiguration configuration, IAppLog appLog)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Missing connection string");

            _appLog = appLog;
        }
        private static Person MapPerson(SqlDataReader reader, in PersonOrdinals ordinals)
        {
            return Person.Load(
                reader.GetInt32(ordinals.PersonId),
                reader.GetString(ordinals.NationalNo),

                reader.GetString(ordinals.FirstName),

                reader.IsDBNull(ordinals.SecondName) ? (string?)null : reader.GetString(ordinals.SecondName),
                reader.IsDBNull(ordinals.ThirdName) ? (string?)null : reader.GetString(ordinals.ThirdName),

                reader.GetString(ordinals.LastName),

                reader.GetDateTime(ordinals.DateOfBirth),
                reader.GetByte(ordinals.Gendor),

                reader.IsDBNull(ordinals.Email) ? (string?)null : reader.GetString(ordinals.Email),

                reader.GetInt32(ordinals.NationalityCountryId),

                reader.IsDBNull(ordinals.ImagePath) ? (string?)null : reader.GetString(ordinals.ImagePath),

                reader.GetDateTime(ordinals.CreatedAt),

                reader.IsDBNull(ordinals.UpdatedAt) ? (DateTime?)null : reader.GetDateTime(ordinals.UpdatedAt),

                reader.GetInt32(ordinals.CreatedByUserId),

                reader.IsDBNull(ordinals.UpdatedByUserId) ? (int?)null : reader.GetInt32(ordinals.UpdatedByUserId),

                reader.GetBoolean(ordinals.IsActive)
                );
        }
        public int Add(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            int personId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_Add", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NationalNo", SqlDbType.VarChar, 20).Value = person.NationalNo;

                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 15).Value = person.FirstName;

                        command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 15).Value = Global.ToDbNull(person.SecondName);
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 15).Value = Global.ToDbNull(person.ThirdName);

                        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 15).Value = person.LastName;

                        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = person.DateOfBirth;
                        command.Parameters.Add("@Gendor", SqlDbType.TinyInt).Value = person.Gendor;
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Global.ToDbNull(person.Email);
                        command.Parameters.Add("@NationalityCountryId", SqlDbType.Int).Value = person.NationalityCountryId;
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = Global.ToDbNull(person.ImagePath);
                        command.Parameters.Add("@CreatedByUserId", SqlDbType.Int).Value = person.CreatedByUserId;

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            personId = Convert.ToInt32(result);

                            switch (personId)
                            {
                                case -2:
                                    throw new DuplicateRecordException("National number already exists.");

                                case -3:
                                    throw new DuplicateRecordException("Email already exists.");

                                default:
                                    if (personId <= 0)
                                        throw new OperationFailedException(@$"Unexpected result returned from 
                                                                           stored procedure. Result: {personId}");
                                    break;
                            }
                        }
                        else
                        {
                            throw new OperationFailedException("Stored procedure returned a null or invalid result.");
                        }
                    }
                }

                if (personId > 0)
                    _appLog.LogInfo($"New person added to system with Id: {personId}");
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred while adding a new person. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return personId;
        }
        public void Update(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = person.PersonId;
                        command.Parameters.Add("@NationalNo", SqlDbType.VarChar, 20).Value = person.NationalNo;

                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 15).Value = person.FirstName;

                        command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 15).Value = Global.ToDbNull(person.SecondName);
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 15).Value = Global.ToDbNull(person.ThirdName);

                        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 15).Value = person.LastName;

                        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = person.DateOfBirth;
                        command.Parameters.Add("@Gendor", SqlDbType.TinyInt).Value = person.Gendor;
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Global.ToDbNull(person.Email);
                        command.Parameters.Add("@NationalityCountryId", SqlDbType.Int).Value = person.NationalityCountryId;
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = Global.ToDbNull(person.ImagePath);
                        command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime2).Value = Global.ToDbNull(person.UpdatedAt);
                        command.Parameters.Add("@UpdatedByUserId", SqlDbType.Int).Value = Global.ToDbNull(person.UpdatedByUserId);
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = person.IsActive;

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

                if (result == 1)
                {
                    _appLog.LogInfo($"The record for Person Id: {person.PersonId} was updated successfully.");
                    return;
                }

                if (result == 0)
                    throw new NotFoundException($"Person with Id: {person.PersonId} not found.");

                if (result == -2)
                    throw new DuplicateRecordException("National number already exists.");

                if (result == -3)
                    throw new DuplicateRecordException("Email already exists.");

                throw new OperationFailedException($"Unexpected result: {result}");
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in UpdatePerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public void Delete(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonId must be greater than zero.", nameof(personId));

            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_Delete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;
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

                if (result == 0)
                    throw new NotFoundException($"Person with Id {personId} does not exist.");

                if (result == 1)
                {
                    _appLog.LogInfo($"The record for Person Id: {personId} was deleted successfully.");
                    return;
                }

                throw new OperationFailedException($"Unexpected result: {result}");
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in DeletePerson method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Person? GetById(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonId must be greater than zero.", nameof(personId));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_GetById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new PersonOrdinals(reader);

                            return MapPerson(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetPersonInfoByID method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public Person? GetByNationalNo(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                throw new ArgumentException("NationalNo can't be empty", nameof(nationalNo));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_GetByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@NationalNo", SqlDbType.VarChar, 20).Value = nationalNo;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            // Cache all ordinals once

                            var ordinals = new PersonOrdinals(reader);

                            return MapPerson(reader, in ordinals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetByNationalNo method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Person> GetAll()
        {
            var allPeople = new List<Person>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return allPeople;

                            // Cache all ordinals once

                            var ordinals = new PersonOrdinals(reader);

                            while (reader.Read())
                            {
                                allPeople.Add(MapPerson(reader, in ordinals));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in GetAllPeople method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }

            return allPeople;
        }
        public bool Exists(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonId must be greater than zero.", nameof(personId));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_ExistsById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;
                        command.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                        connection.Open();
                        command.ExecuteNonQuery();

                        object returnedValue = command.Parameters["@Result"].Value;

                        // Ensure output parameter was assigned in SP
                        if (returnedValue == DBNull.Value)
                            throw new OperationFailedException("Output parameter Result was not assigned.");

                        return Convert.ToBoolean(returnedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in Exists(personId) method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
        public bool Exists(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                throw new ArgumentException("NationalNo cannot be empty.", nameof(nationalNo));

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_Person_ExistsByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@NationalNo", SqlDbType.VarChar, 20).Value = nationalNo;
                        command.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                        connection.Open();
                        command.ExecuteNonQuery();

                        object returnedValue = command.Parameters["@Result"].Value;

                        // Ensure output parameter was assigned in SP
                        if (returnedValue == DBNull.Value)
                            throw new OperationFailedException("Output parameter Result was not assigned.");

                        return Convert.ToBoolean(returnedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                _appLog.LogError($"Error occurred in Exists(nationalNo) method. Details: {ex.Message} | StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}