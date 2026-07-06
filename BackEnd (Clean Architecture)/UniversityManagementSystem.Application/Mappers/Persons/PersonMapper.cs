using UniversityManagementSystem.Application.Response_DTO.Persons;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Mappers.Persons
{
    public static class PersonMapper
    {
        public static PersonDTO ToDto(Person person)
        {
            return new PersonDTO(
                person.PersonId,
                person.NationalNo,
                person.FirstName,
                person.SecondName,
                person.ThirdName,
                person.LastName,
                person.DateOfBirth,
                person.Gendor,
                person.Email,
                person.NationalityCountryId,
                person.ImagePath,
                person.CreatedAt,
                person.UpdatedAt,
                person.CreatedByUserId,
                person.UpdatedByUserId,
                person.IsActive
                );
        }
    }
}