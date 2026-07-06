using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Mappers.Persons;
using UniversityManagementSystem.Application.Response_DTO.Persons;

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
            var allPeople = _personRepository.GetAll();

            return allPeople.Select(PersonMapper.ToDto).ToList();
            //return allPeople.Select(a => PersonMapper.ToDto(a)).ToList();
        }
    }
}