using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Mappers.Addresses;
using UniversityManagementSystem.Application.Response_DTO.Addresses;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class GetAddressesByPersonIdUseCase
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressesByPersonIdUseCase(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public List<AddressDTO> Execute(int personId)
        {
            var addresses = _addressRepository.GetByPersonId(personId);

            return addresses.Select(AddressMapper.ToDto).ToList();
        }
    }
}