using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class GetPersonByNationalNoUseCase
    {
        private readonly IPersonRepository _personRepository;
        public GetPersonByNationalNoUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public PersonDTO Execute(string nationalNo)
        {
            var person = _personRepository.GetByNationalNo(nationalNo);

            if (person == null)
                throw new NotFoundException($"Person with national number: {nationalNo} not found");

            return new PersonDTO(person.PersonId, person.NationalNo, person.FirstName, person.SecondName, person.ThirdName,
                person.LastName, person.DateOfBirth, person.Gendor, person.Email, person.NationalityCountryId, person.ImagePath,
                person.CreatedDate, person.LastStatusDate, person.CreatedByAdminId);
        }
    }
}