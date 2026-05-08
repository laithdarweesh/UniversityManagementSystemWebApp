using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; private set; }
        public string CountryName { get; private set; }
        private Country() { }
        private Country(int CountryId, string CountryName) 
        {
            _ValidateCountryName(CountryName);

            this.CountryId = CountryId;
            this.CountryName = CountryName;
        }
        public static Country Add(string CountryName)
        {
            return new Country(0, CountryName);
        }
        public void SetCountryName(string NewCountryName)
        {
            _ValidateCountryName(NewCountryName);
            this.CountryName = NewCountryName;
        }
        private void _ValidateCountryName(string CountryName)
        {
            if(string.IsNullOrWhiteSpace(CountryName))
                throw new ArgumentException("Country name is required");   
        }
    }
}