using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Country
{
    public class GetAllCountriesUseCase
    {
        private readonly ICountryRepository _CountryRepository;
        public GetAllCountriesUseCase(ICountryRepository CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }
        public List<CountryDTO> Execute()
        {
            var AllCountries = _CountryRepository.GetAllCountries();

            return AllCountries.Select(c => 
                        new CountryDTO (c.CountryId, c.CountryName)).ToList();
        }
    }
}