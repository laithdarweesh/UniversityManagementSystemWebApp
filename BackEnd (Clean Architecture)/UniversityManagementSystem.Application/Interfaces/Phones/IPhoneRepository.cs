using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Phones
{
    public interface IPhoneRepository
    {
        int Add(Phone phone);
        void Update(Phone phone);
        Phone? GetById(int phoneId);
        Phone? GetByPhoneNumber(string phoneNumber);
        List<Phone> GetByPersonId(int personId);
        List<Phone> GetAll();
    }
}