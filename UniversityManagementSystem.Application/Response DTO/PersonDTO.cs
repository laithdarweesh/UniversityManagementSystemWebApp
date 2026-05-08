using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Response_DTO
{
    public class PersonDTO
    {
        public int PersonId { get; }
        public string NationalNo { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public string ThirdName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public byte Gendor { get; }
        public string Email { get; }
        public int NationalityCountryId { get; }
        public string ImagePath { get; }
        public DateTime CreatedDate { get; }
        public DateTime LastStatusDate { get; }
        public int CreatedByAdminId { get; }
        public PersonDTO(int PersonId, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Email, int NationalityCountryId, string ImagePath, DateTime CreatedDate,
            DateTime LastStatusDate, int CreatedByAdminId)
        {
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
    }
}