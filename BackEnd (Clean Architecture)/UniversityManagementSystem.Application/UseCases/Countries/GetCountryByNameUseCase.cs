using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Mappers.Countries;
using UniversityManagementSystem.Application.Response_DTO.Countries;

namespace UniversityManagementSystem.Application.UseCases.Countries
{
    public class GetCountryByNameUseCase
    {
        private readonly ICountryRepository _countryRepository;
        public GetCountryByNameUseCase(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public CountryDTO Execute(string countryName)
        {
            var country = _countryRepository.GetByName(countryName);

            if (country == null)
                throw new NotFoundException($"{countryName} Country not found");

            return CountryMapper.ToDto(country);
        }
    }
}