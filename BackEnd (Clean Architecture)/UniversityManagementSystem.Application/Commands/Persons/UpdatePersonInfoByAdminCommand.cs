namespace UniversityManagementSystem.Application.Commands.Persons
{
    public class UpdatePersonInfoByAdminCommand
    {
        public string? NationalNo { get; private set; }
        public string? FirstName { get; private set; }
        public string? SecondName { get; private set; }
        public string? ThirdName { get; private set; }
        public string? LastName { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public byte? Gendor { get; private set; }
        public int? NationalityCountryId { get; private set; }
        public UpdatePersonInfoByAdminCommand(string? nationalNo, string? firstName, string? secondName, string? thirdName,
            string? lastName, DateTime? dateOfBirth, byte? gendor, int? nationalityCountryId)
        {
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            NationalityCountryId = nationalityCountryId;
        }
    }
}