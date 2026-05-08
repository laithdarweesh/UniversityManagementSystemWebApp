using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Addresses
{
    public interface IAddressRepository
    {
        int Add(Address Address);
        bool Update(Address Address);
        Address GetById(int AddressId);
        List<Address> GetAddressesByPersonID(int PersonId);
        List<Address> GetAllAddresses();
    }
}