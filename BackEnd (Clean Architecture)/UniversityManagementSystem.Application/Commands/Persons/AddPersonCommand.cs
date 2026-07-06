namespace UniversityManagementSystem.Application.Commands.Persons
{
    public class AddPersonCommand
    {
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
        public int CreatedByUserId { get; }
        public AddPersonCommand(string nationalNo, string firstName, string? secondName, string? thirdName, string lastName,
                                DateTime dateOfBirth, byte gendor, string? email, int nationalityCountryId, string? imagePath,
                                int createdByUserId)
        {
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
            this.CreatedByUserId = createdByUserId;
        }
    }
}