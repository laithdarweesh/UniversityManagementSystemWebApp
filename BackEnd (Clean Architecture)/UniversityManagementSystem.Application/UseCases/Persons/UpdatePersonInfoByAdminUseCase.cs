using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class UpdatePersonInfoByAdminUseCase
    {
        private readonly IPersonRepository _personRepository;
        public UpdatePersonInfoByAdminUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void Execute(int personId, UpdatePersonInfoByAdminCommand command, int userId)
        {
            bool hasChanged = false;
            var person = _personRepository.GetById(personId);

            if (person == null)
                throw new NotFoundException($"Person with id: {personId} not found");

            //Update national no

            if (command.NationalNo != null)
            {
                person.CorrectNationalNumber(command.NationalNo);
                hasChanged = true;
            }

            //Update person name

            if (command.FirstName != null || command.SecondName != null
                || command.ThirdName != null || command.LastName != null)
            {
                person.CorrectPersonName(
                                         command.FirstName ?? person.FirstName,
                                         command.SecondName ?? person.SecondName,
                                         command.ThirdName ?? person.ThirdName,
                                         command.LastName ?? person.LastName
                                         );

                hasChanged = true;
            }

            //Update date of birth

            if (command.DateOfBirth.HasValue)
            {
                person.CorrectDateOfBirth(command.DateOfBirth.Value);
                hasChanged = true;
            }

            //Update gendor 

            if (command.Gendor.HasValue)
            {
                person.CorrectGendor(command.Gendor.Value);
                hasChanged = true;
            }

            //Update nationality

            if (command.NationalityCountryId.HasValue)
            {
                person.CorrectNationality(command.NationalityCountryId.Value);
                hasChanged = true;
            }

            //Mark as updated

            if (hasChanged)
            {
                person.MarkAsUpdated(userId);
                _personRepository.Update(person);
            }
        }
    }
}