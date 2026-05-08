using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class UpdatePersonUeCase
    {
        private readonly IPersonRepository _personRepository;
        public UpdatePersonUeCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void Execute(int personId, UpdatePersonCommand command)
        {
            var person = _personRepository.GetByPersonId(personId);

            if (person == null)
                throw new NotFoundException($"Person with id: {personId} not found");

            if (command.Email != null)
                person.SetEmail(command.Email);

            if (command.ImagePath != null)
                person.SetImagePath(command.ImagePath);

            bool updated = _personRepository.Update(person);

            if (!updated)
                throw new Exception("Failed to update person info");
        }
    }
}