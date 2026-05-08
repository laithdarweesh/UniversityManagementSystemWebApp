using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class GetAllPeopleUseCase
    {
        private readonly IPersonRepository _personRepository;
        public GetAllPeopleUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public List<PersonDTO> Execute()
        {
            var allPeople = _personRepository.GetAllPeople();

            return allPeople.Select(a => new PersonDTO(a.PersonId, a.NationalNo, a.FirstName, a.SecondName, a.ThirdName,
                a.LastName, a.DateOfBirth, a.Gendor, a.Email, a.NationalityCountryId, a.ImagePath,
                a.CreatedDate, a.LastStatusDate, a.CreatedByAdminId)).ToList();
        }
    }
}