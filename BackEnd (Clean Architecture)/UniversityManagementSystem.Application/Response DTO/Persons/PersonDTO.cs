namespace UniversityManagementSystem.Application.Response_DTO.Persons
{
    public class PersonDTO
    {
        public int PersonId { get; }
        public string NationalNo { get; }
        public string FirstName { get; }
        public string? SecondName { get; }
        public string? ThirdName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public byte Gendor { get; }
        public string? Email { get; }
        public int NationalityCountryId { get; }
        public string? ImagePath { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
        public int CreatedByUserId { get; }
        public int? UpdatedByUserId { get; }
        public bool IsActive { get; }
        public PersonDTO(int personId, string nationalNo, string firstName, string? secondName, string? thirdName, string lastName,
                    DateTime dateOfBirth, byte gendor, string? email, int nationalityCountryId, string? imagePath, DateTime createdAt,
                    DateTime? updatedAt, int createdByUserId, int? updatedByUserId, bool isActive)
        {
            this.PersonId = personId;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Email = email;
            this.NationalityCountryId = nationalityCountryId;
            this.ImagePath = imagePath;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.CreatedByUserId = createdByUserId;
            this.UpdatedByUserId = updatedByUserId;
            this.IsActive = isActive;
        }
    }
}