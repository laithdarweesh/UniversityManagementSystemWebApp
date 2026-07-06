using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Mappers.Addresses;
using UniversityManagementSystem.Application.Response_DTO.Addresses;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class GetAddressByIdUseCase
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressByIdUseCase(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public AddressDTO Execute(int addressId)
        {
            var address = _addressRepository.GetById(addressId);

            if (address == null)
                throw new NotFoundException($"Address with id: {addressId} not found");

            return AddressMapper.ToDto(address);
        }
    }
}