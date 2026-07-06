using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Addresses
{
    public interface IAddressRepository
    {
        int Add(Address address);
        void Update(Address address);
        Address? GetById(int addressId);
        List<Address> GetByPersonId(int personId);
        List<Address> GetAll();
    }
}