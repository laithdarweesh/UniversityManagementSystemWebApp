using UniversityManagementSystem.Application.Response_DTO.Phones;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Mappers.Phones
{
    public static class PhoneMapper
    {
        public static PhoneDTO ToDto(Phone phone)
        {
            return new PhoneDTO
            (
                phone.PhoneId,
                phone.PhoneNumber,
                phone.PersonId
            );
        }
    }
}