using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.Application.Interfaces.Addresses;

using AddressEntity = UniversityManagementSystem.Domain.Entities.Address;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class AddAddressUseCase
    {
        private readonly IAddressRepository _AddressRepository;
        public AddAddressUseCase(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository;
        }
        public int Execute(AddAddressCommand AddAddressCommand)
        {
            var address = AddressEntity.Add(AddAddressCommand.AddressName, AddAddressCommand.PersonId);
            return _AddressRepository.Add(address);
        }
    }
}