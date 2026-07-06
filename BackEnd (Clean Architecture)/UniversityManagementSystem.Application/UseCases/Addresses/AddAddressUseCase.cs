using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Addresses;

using AddressEntity = UniversityManagementSystem.Domain.Entities.Address;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class AddAddressUseCase
    {
        private readonly IAddressRepository _addressRepository;
        public AddAddressUseCase(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public int Execute(AddAddressCommand command)
        {
            var address = AddressEntity.Create(command.AddressName, command.PersonId);
            int addressId = _addressRepository.Add(address);

            if (addressId <= 0)
                throw new OperationFailedException("Failed to create address");

            return addressId;
        }
    }
}