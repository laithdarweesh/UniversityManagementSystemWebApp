using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class UpdateAddressUseCase
    {
        private readonly IAddressRepository _AddressRepository;
        public UpdateAddressUseCase(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository;
        }
        public void Execute(int AddressId, UpdateAddressCommand UpdateAddressCommand)
        {
            var Address = _AddressRepository.GetById(AddressId);

            if (Address == null)
                throw new NotFoundException("Address not found");

            Address.SetAddressName(UpdateAddressCommand.AddressName);
            bool Updated = _AddressRepository.Update(Address);

            if (!Updated)
                throw new Exception("Failed to update address name");
        }
    }
}