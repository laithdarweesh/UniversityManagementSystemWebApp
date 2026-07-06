using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Mappers.Persons;
using UniversityManagementSystem.Application.Response_DTO.Persons;

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

            return PersonMapper.ToDto(person);
        }
    }
}