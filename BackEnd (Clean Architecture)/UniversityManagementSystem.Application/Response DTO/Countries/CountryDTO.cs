namespace UniversityManagementSystem.Application.Response_DTO.Countries
{
    public class CountryDTO
    {
        public int CountryId { get; }
        public string CountryName { get; }
        public CountryDTO(int countryId, string countryName)
        {
            this.CountryId = countryId;
            this.CountryName = countryName;
        }
    }
}