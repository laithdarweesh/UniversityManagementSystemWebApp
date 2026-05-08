using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.UseCases.Country
{
    public class GetCountryByIdUseCase
    {
        private readonly ICountryRepository _CountryRepository;
        public GetCountryByIdUseCase(ICountryRepository CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }
        public CountryDTO Execute(int CountryId)
        {
            var Country = _CountryRepository.GetCountry(CountryId);

            if (Country == null)
                throw new NotFoundException($"Country with id: {CountryId} not found");

            return new CountryDTO(Country.CountryId, Country.CountryName);
        }
    }
}
