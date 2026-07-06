using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Mappers.Countries;
using UniversityManagementSystem.Application.Response_DTO.Countries;

namespace UniversityManagementSystem.Application.UseCases.Countries
{
    public class GetAllCountriesUseCase
    {
        private readonly ICountryRepository _countryRepository;
        public GetAllCountriesUseCase(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public List<CountryDTO> Execute()
        {
            var allCountries = _countryRepository.GetAll();

            return allCountries.Select(CountryMapper.ToDto).ToList();
        }
    }
}