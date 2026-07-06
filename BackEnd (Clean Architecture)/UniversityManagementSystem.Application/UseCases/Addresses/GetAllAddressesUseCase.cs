using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Mappers.Addresses;
using UniversityManagementSystem.Application.Response_DTO.Addresses;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class GetAllAddressesUseCase
    {
        private readonly IAddressRepository _addressRepository;
        public GetAllAddressesUseCase(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public List<AddressDTO> Execute()
        {
            var allAddresses = _addressRepository.GetAll();

            return allAddresses.Select(AddressMapper.ToDto).ToList();
        }
    }
}