using UniversityManagementSystem.Application.Response_DTO.Addresses;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Mappers.Addresses
{
    public static class AddressMapper
    {
        public static AddressDTO ToDto(Address address)
        {
            return new AddressDTO
            (
                address.AddressId,
                address.AddressName,
                address.PersonId
            );
        }
    }
}