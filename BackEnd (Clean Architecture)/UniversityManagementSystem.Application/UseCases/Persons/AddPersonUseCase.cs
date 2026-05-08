using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class AddPersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        public AddPersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public int Execute(AddPersonCommand command)
        {
            if (!_personRepository.IsPersonExist(command.NationalNo))
                throw new ArgumentException("Person with this national number already exists");

            var person = Person.Add(command.NationalNo, command.FirstName, command.SecondName,
                command.ThirdName, command.LastName, command.DateOfBirth, command.Gendor,
                command.Email, command.NationalityCountryId, command.ImagePath, command.CreatedDate,
                command.LastStatusDate, command.CreatedByAdminId);

            return _personRepository.Add(person);
        }
    }
}