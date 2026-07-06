using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Mappers.Persons;
using UniversityManagementSystem.Application.Response_DTO.Persons;

namespace UniversityManagementSystem.Application.UseCases.Persons
{
    public class GetPersonByIdUseCase
    {
        private readonly IPersonRepository _personRepository;
        public GetPersonByIdUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public PersonDTO Execute(int personId)
        {
            var person = _personRepository.GetById(personId);

            if (person == null)
                throw new NotFoundException($"Person with id: {personId} not found");

            return PersonMapper.ToDto(person);
        }
    }
}