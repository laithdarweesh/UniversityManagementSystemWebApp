namespace UniversityManagementSystem.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; private set; }
        public string CountryName { get; private set; }
        private Country() { }
        private Country(int countryId, string countryName)
        {
            if (countryId < 0)
                throw new ArgumentException("CountryId cannot be negative", nameof(countryId));

            _ValidateCountryName(countryName);

            this.CountryId = countryId;
            this.CountryName = countryName;
        }

        /// <summary>
        /// Loads an existing Country entity from database data.
        /// Used by repositories when reconstructing domain objects.
        /// </summary>
        public static Country Load(int countryId, string countryName)
        {
            return new Country(countryId, countryName);
        }
        private static void _ValidateCountryName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentException("Country name is required");
        }
    }
}