using UniversityManagementSystem.Application.Response_DTO.Countries;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Mappers.Countries
{
    public static class CountryMapper
    {
        public static CountryDTO ToDto(Country country)
        {
            return new CountryDTO
            (
                country.CountryId,
                country.CountryName
            );
        }
    }
}