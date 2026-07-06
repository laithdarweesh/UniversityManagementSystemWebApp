using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Phones
{
    public class AddPhoneRequest
    {
        [StringLength(20)]
        public required string PhoneNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PersonId { get; set; }
    }
}