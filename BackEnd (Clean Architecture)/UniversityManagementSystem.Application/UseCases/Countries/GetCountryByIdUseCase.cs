using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Mappers.Countries;
using UniversityManagementSystem.Application.Response_DTO.Countries;

namespace UniversityManagementSystem.Application.UseCases.Countries
{
    public class GetCountryByIdUseCase
    {
        private readonly ICountryRepository _countryRepository;
        public GetCountryByIdUseCase(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public CountryDTO Execute(int countryId)
        {
            var country = _countryRepository.GetById(countryId);

            if (country == null)
                throw new NotFoundException($"Country with id: {countryId} not found");

            return CountryMapper.ToDto(country);
        }
    }
}
