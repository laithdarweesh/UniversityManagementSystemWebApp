using UniversityManagementSystem.Domain.Shared.Guards;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; private set; }
        public string NationalNo { get; private set; }
        public string FirstName { get; private set; }
        public string? SecondName { get; private set; }
        public string? ThirdName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public byte Gendor { get; private set; }
        public string? Email { get; private set; }
        public int NationalityCountryId { get; private set; }
        public string? ImagePath { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int CreatedByUserId { get; private set; }
        public int? UpdatedByUserId { get; private set; }
        public bool IsActive { get; private set; }
        public string FullName => string.Join(" ",
                new[]
                {
                    FirstName,
                    SecondName,
                    ThirdName,
                    LastName
                }.Where(x => !string.IsNullOrWhiteSpace(x)));
        private Person() { }
        private Person(int personId, string nationalNo, string firstName, string? secondName, string? thirdName,
                       string lastName, DateTime dateOfBirth, byte gendor, string? email, int nationalityCountryId,
                       string? imagePath, DateTime createdAt, DateTime? updatedAt, int createdByUserId,
                       int? updatedByUserId, bool isActive)
        {
            _ValidateNationalNumber(nationalNo);
            _ValidateName(firstName, secondName, thirdName, lastName);
            _ValidateDateOfBirth(dateOfBirth);
            _ValidateGender(gendor);
            _ValidateEmail(email);
            _ValidateImagePath(imagePath);

            Ensure.ValidatePositiveId(nationalityCountryId, nameof(nationalityCountryId));
            Ensure.ValidatePositiveId(createdByUserId, nameof(createdByUserId));

            if (personId < 0)
                throw new ArgumentException("PersonId cannot be negative", nameof(personId));

            if (updatedByUserId.HasValue)
                Ensure.ValidatePositiveId(updatedByUserId.Value, nameof(updatedByUserId));

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

        /// <summary>
        /// Creates a new Person entity before saving it to database.
        /// PersonId is initialized with 0 because the database
        /// will generate the identity value.
        /// </summary>
        public static Person Create(string nationalNo, string firstName, string? secondName, string? thirdName, string lastName,
                                 DateTime dateOfBirth, byte gendor, string? email, int nationalityCountryId, string? imagePath,
                                 int createdByUserId)
        {
            return new Person(0, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gendor, email,
                              nationalityCountryId, imagePath, DateTime.UtcNow, null, createdByUserId, null, true);
        }

        /// <summary>
        /// Loads an existing Person entity from database data.
        /// Used by repositories when reconstructing domain objects.
        /// </summary>
        public static Person Load(int personId, string nationalNo, string firstName, string? secondName, string? thirdName,
                                  string lastName, DateTime dateOfBirth, byte gendor, string? email, int nationalityCountryId,
                                  string? imagePath, DateTime createdAt, DateTime? updatedAt, int createdByUserId,
                                  int? updatedByUserId, bool isActive)
        {
            return new Person(personId, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gendor, email,
                              nationalityCountryId, imagePath, createdAt, updatedAt, createdByUserId, updatedByUserId, isActive);
        }

        //Set Email
        public void SetEmail(string? newEmail)
        {
            _ValidateEmail(newEmail);
            this.Email = newEmail;
        }

        //Set Image Path
        public void SetImagePath(string? newImagePath)
        {
            _ValidateImagePath(newImagePath);
            this.ImagePath = newImagePath;
        }

        /*Methods for correcting administrative errors, used by admin only:*/

        //Correct Person Name
        public void CorrectPersonName(string firstName, string? secondName, string? thirdName, string lastName)
        {
            _ValidateName(firstName, secondName, thirdName, lastName);

            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
        }

        //Correct national number
        public void CorrectNationalNumber(string nationalNo)
        {
            _ValidateNationalNumber(nationalNo);
            this.NationalNo = nationalNo;
        }

        //Correct DateOfBirth:
        public void CorrectDateOfBirth(DateTime dateOfBirth)
        {
            _ValidateDateOfBirth(dateOfBirth);
            this.DateOfBirth = dateOfBirth;
        }

        //Correct Gendor:
        public void CorrectGendor(byte gendor)
        {
            _ValidateGender(gendor);
            this.Gendor = gendor;
        }

        //Correct NationalityCountryId
        public void CorrectNationality(int nationalityCountryId)
        {
            Ensure.ValidatePositiveId(nationalityCountryId, nameof(nationalityCountryId));
            this.NationalityCountryId = nationalityCountryId;
        }

        public void MarkAsUpdated(int userId)
        {
            Ensure.ValidatePositiveId(userId, nameof(userId));

            this.UpdatedAt = DateTime.UtcNow;
            this.UpdatedByUserId = userId;
        }

        //Validation Methods:

        //Validate Person Name
        private static void _ValidateName(string firstName, string? secondName, string? thirdName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required");

            if (firstName.Any(char.IsDigit) || (secondName is not null && secondName.Any(char.IsDigit))
                    || (thirdName is not null && thirdName.Any(char.IsDigit)) || lastName.Any(char.IsDigit))
            {
                throw new ArgumentException("Name cannot contain digits");
            }

            if (firstName.Length > 15 || (secondName is not null && secondName.Length > 15)
                || (thirdName is not null && thirdName.Length > 15) || lastName.Length > 15)
                throw new ArgumentException("Name is too long");
        }

        //Validate National No
        private static void _ValidateNationalNumber(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                throw new ArgumentException("NationalNo is required");

            if (!Validation.ValidateInteger(nationalNo) || nationalNo.Length > 20)
                throw new ArgumentException("Invalid national number");
        }

        //Validate Email
        private static void _ValidateEmail(string? email)
        {
            if (email is not null && email.Length > 50)
                throw new ArgumentException("Email is too long");

            if (email is not null && !Validation.ValidateEmail(email))
                throw new ArgumentException("Invalid email address");
        }

        //Validate ImagePath
        private static void _ValidateImagePath(string? imagePath)
        {
            if (imagePath is not null && imagePath.Length > 250)
                throw new ArgumentException("Image path is too long");

            //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            //if (!allowedExtensions.Any(ext => imagePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            //    throw new ArgumentException("Invalid image format");
        }

        //Validate DateOfBirth
        private static void _ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Invalid date, date of birth cannot be in the future");

            short age = (short)(DateTime.UtcNow.Year - dateOfBirth.Year);

            // Calculate age based on year difference.
            // If the birth date has not occurred yet this year,
            // subtract one year to get the correct age.
            // Date comparison works by checking year, then month, then day.

            if (dateOfBirth.Date > DateTime.UtcNow.AddYears(-age).Date)
                age--;

            if (age < 18)
                throw new ArgumentException("Person must be at least 18 years old");

            if (age > 65)
                throw new ArgumentException("Age exceeded the maximum age limit");
        }

        //Validate Gender
        private static void _ValidateGender(byte gendor)
        {
            if (gendor != 0 && gendor != 1)
                throw new ArgumentException("Invalid gender");
        }
    }
}