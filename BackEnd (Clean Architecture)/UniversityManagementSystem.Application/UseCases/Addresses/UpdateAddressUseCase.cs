using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Addresses;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class UpdateAddressUseCase
    {
        private readonly IAddressRepository _addressRepository;
        public UpdateAddressUseCase(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public void Execute(int addressId, UpdateAddressCommand command, int userId)
        {
            var address = _addressRepository.GetById(addressId);

            if (address == null)
                throw new NotFoundException("Address not found");

            //Update addressName

            if (command.AddressName != null)
                address.SetAddressName(command.AddressName);

            _addressRepository.Update(address);
        }
    }
}