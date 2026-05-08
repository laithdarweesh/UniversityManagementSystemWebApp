using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; private set; }
        public string NationalNo { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string ThirdName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public byte Gendor { get; private set; }
        public string Email { get; private set; }
        public int NationalityCountryId { get; private set; }
        public string ImagePath { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastStatusDate { get; private set; }
        public int CreatedByAdminId { get; private set; }

        //About Phones & Addresses
        //public clsPhone Phone { get; private set; }
        //public clsAddress Address { get; private set; }
        //public List<clsPhone> AllPhones { get; private set; }
        //public List<clsAddress> AllAddresses { get; private set; }
        //public clsPhone MainPhone => AllPhones?.FirstOrDefault();
        //public clsAddress MainAddress => AllAddresses?.FirstOrDefault();
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        private Person() { }
        private Person(int PersonId, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Email, int NationalityCountryId, string ImagePath, DateTime CreatedDate,
            DateTime LastStatusDate, int CreatedByAdminId)
        {
            _ValidateNationalNumber(NationalNo);
            _ValidateName(FirstName, SecondName, ThirdName, LastName);
            _ValidateDateOfBirth(DateOfBirth);
            _ValidateEmail(Email);
            _ValidateImagePath(ImagePath);

            this.PersonId = PersonId;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Email = Email;
            this.NationalityCountryId = NationalityCountryId;
            this.ImagePath = ImagePath;
            this.CreatedDate = CreatedDate;
            this.LastStatusDate = LastStatusDate;
            this.CreatedByAdminId = CreatedByAdminId;
        }
        public static Person Add(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Email, int NationalityCountryId, string ImagePath, DateTime CreatedDate,
            DateTime LastStatusDate, int CreatedByAdminId)
        {
            return new Person(0, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Email,
                NationalityCountryId, ImagePath, CreatedDate, LastStatusDate, CreatedByAdminId);
        }

        //Set Email
        public void SetEmail(string NewEmail)
        {
            _ValidateEmail(NewEmail);
            this.Email = NewEmail;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Set Image Path
        public void SetImagePath(string NewImagePath)
        {
            _ValidateImagePath(NewImagePath);
            this.ImagePath = NewImagePath;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Update Last Status Date
        private void _UpdateLastModifiedDate()
        {
            this.LastStatusDate = DateTime.UtcNow;
        }

        /*Methods for correcting administrative errors, used by admin only:*/

        //Correct Person Name
        public void CorrectPersonName(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            _ValidateName(FirstName, SecondName, ThirdName, LastName);

            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Correct national number
        public void CorrectNationalNumber(string NationalNo)
        {
            _ValidateNationalNumber(NationalNo);
            this.NationalNo = NationalNo;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Correct DateOfBirth:
        public void CorrectDateOfBirth(DateTime DateOfBirth)
        {
            _ValidateDateOfBirth(DateOfBirth);
            this.DateOfBirth = DateOfBirth;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Correct Gendor:
        public void CorrectGendor(byte Gendor)
        {
            this.Gendor = Gendor;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Correct NationalityCountryId
        public void CorrectNationalityCountryId(int NationalityCountryId)
        {
            this.NationalityCountryId = NationalityCountryId;

            //Update Last Status Date
            _UpdateLastModifiedDate();
        }

        //Validation Methods:

        //Validate Person Name
        private void _ValidateName(string FirstName, string SecondName, string ThirdName, string LastName)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(SecondName))
                throw new ArgumentException("Second name is required");

            if (string.IsNullOrWhiteSpace(ThirdName))
                throw new ArgumentException("Third name is required");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Last name is required");
        }

        //Validate National No
        private void _ValidateNationalNumber(string NationalNo)
        {
            if (!clsValidation.ValidateInteger(NationalNo) || NationalNo.Length > 9)
                throw new ArgumentException("Invalid national number");
        }

        //Validate Email
        private void _ValidateEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email is required");

            if (!clsValidation.ValidateEmail(Email))
                throw new ArgumentException("Invalid email address");
        }

        //Validate ImagePath
        private void _ValidateImagePath(string ImagePath)
        {
            if (string.IsNullOrWhiteSpace(ImagePath))
                throw new ArgumentException("Image Path is required");

            if (ImagePath.Length > 250)
                throw new ArgumentException("Image path is too long");

            //var AllowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            //if (!AllowedExtensions.Any(ext => ImagePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            //    throw new ArgumentException("Invalid image format");
        }

        //Validate DateOfBirth
        private void _ValidateDateOfBirth(DateTime DateOfBirth)
        {
            if (DateOfBirth.Date > DateTime.UtcNow.Date)
                throw new ArgumentException("Invalid date, date of birth cannot be in the future");

            short Age = (short)(DateTime.UtcNow.Year - DateOfBirth.Year);

            // Calculate age based on year difference.
            // If the birth date has not occurred yet this year,
            // subtract one year to get the correct age.
            // Date comparison works by checking year, then month, then day.

            if (DateOfBirth.Date > DateTime.UtcNow.AddYears(-Age).Date)
                Age--;

            if (Age < 18)
                throw new ArgumentException("Person must be at least 18 years old");

            if (Age > 65)
                throw new ArgumentException("Age exceeded the maximum age limit");
        }
    }
}