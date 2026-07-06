using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        public UpdatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void Execute(int personId, UpdatePersonCommand command, int userId)
        {
            bool hasChanged = false;
            var person = _personRepository.GetById(personId);

            if (person == null)
                throw new NotFoundException($"Person with id: {personId} not found");

            //Update email

            if (command.Email != null)
            {
                person.SetEmail(command.Email);
                hasChanged = true;
            }

            //Update imagePath

            if (command.ImagePath != null)
            {
                person.SetImagePath(command.ImagePath);
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