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
        public void Execute(int personId) => _personRepository.Delete(personId);
    }
}