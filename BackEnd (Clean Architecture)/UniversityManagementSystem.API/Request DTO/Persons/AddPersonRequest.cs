using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Persons
{
    public class AddPersonRequest
    {
        [StringLength(20)]
        public required string NationalNo { get; set; }

        [StringLength(15)]
        public required string FirstName { get; set; }

        [StringLength(15)]
        public string? SecondName { get; set; }

        [StringLength(15)]
        public string? ThirdName { get; set; }

        [StringLength(15)]
        public required string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, 1)]
        public byte Gendor { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NationalityCountryId { get; set; }

        [StringLength(250)]
        public string? ImagePath { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CreatedByUserId { get; set; }
    }
}