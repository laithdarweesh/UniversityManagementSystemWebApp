namespace UniversityManagementSystem.API.Request_DTO.Persons
{
    public class AddPersonRequest
    {
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Email { get; set; }
        public int NationalityCountryId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public int CreatedByAdminId { get; set; }
    }
}
