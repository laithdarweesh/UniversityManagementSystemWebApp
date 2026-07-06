using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Persons
{
    public class UpdatePersonRequest
    {
        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? ImagePath { get; set; }
    }
}