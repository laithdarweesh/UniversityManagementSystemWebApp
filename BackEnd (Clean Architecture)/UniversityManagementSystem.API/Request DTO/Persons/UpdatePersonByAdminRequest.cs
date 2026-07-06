using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Persons
{
    public class UpdatePersonByAdminRequest
    {
        [StringLength(20)]
        public string? NationalNo { get; set; }

        [StringLength(15)]
        public string? FirstName { get; set; }

        [StringLength(15)]
        public string? SecondName { get; set; }

        [StringLength(15)]
        public string? ThirdName { get; set; }

        [StringLength(15)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Range(0, 1)]
        public byte? Gendor { get; set; }

        [Range(1, int.MaxValue)]
        public int? NationalityCountryId { get; set; }
    }
}
