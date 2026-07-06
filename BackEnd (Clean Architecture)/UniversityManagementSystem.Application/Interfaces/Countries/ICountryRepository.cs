using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Countries
{
    public interface ICountryRepository
    {
        Country? GetById(int countryId);
        Country? GetByName(string countryName);
        List<Country> GetAll();
    }
}