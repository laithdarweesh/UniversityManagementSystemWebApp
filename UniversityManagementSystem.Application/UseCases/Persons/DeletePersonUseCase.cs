using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        public DeletePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void Execute(int personId)
        {
            bool deleted = _personRepository.Delete(personId);

            if (!deleted)
                throw new NotFoundException($"Person with id {personId} not found");
        }
    }
}