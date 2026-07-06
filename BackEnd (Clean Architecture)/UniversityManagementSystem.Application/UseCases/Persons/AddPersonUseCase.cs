using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Common.Exceptions;
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
            var person = Person.Create(command.NationalNo, command.FirstName, command.SecondName,
                                       command.ThirdName, command.LastName, command.DateOfBirth,
                                       command.Gendor, command.Email, command.NationalityCountryId,
                                       command.ImagePath, command.CreatedByUserId);

            int personId = _personRepository.Add(person);

            if (personId <= 0)
                throw new OperationFailedException("Failed to create person");

            return personId;
        }
    }
}