using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Countries
{
    public interface ICountryRepository
    {
        Country GetCountry(int CountryId);
        Country GetCountry(string CountryName);
        List<Country> GetAllCountries();
    }
}