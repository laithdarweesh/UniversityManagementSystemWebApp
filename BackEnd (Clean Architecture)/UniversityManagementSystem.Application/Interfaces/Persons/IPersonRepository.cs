using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Persons
{
    public interface IPersonRepository
    {
        int Add(Person person);
        void Update(Person person);
        void Delete(int personId);
        Person? GetById(int personId);
        Person? GetByNationalNo(string nationalNo);
        List<Person> GetAll();
        bool Exists(int personId);
        bool Exists(string nationalNo);
    }
}