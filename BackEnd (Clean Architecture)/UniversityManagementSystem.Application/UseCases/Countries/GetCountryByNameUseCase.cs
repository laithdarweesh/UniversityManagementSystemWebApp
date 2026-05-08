using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Country
{
    public class GetCountryByNameUseCase
    {
        private readonly ICountryRepository _CountryRepository;
        public GetCountryByNameUseCase(ICountryRepository CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }
        public CountryDTO Execute(string CountryName)
        {
            var Country = _CountryRepository.GetCountry(CountryName);

            if (Country == null)
                throw new NotFoundException($"{CountryName} Country not found");

            return new CountryDTO(Country.CountryId, Country.CountryName);
        }
    }
}
