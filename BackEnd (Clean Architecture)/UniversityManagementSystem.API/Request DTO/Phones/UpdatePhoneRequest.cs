using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Phones
{
    public class UpdatePhoneRequest
    {
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}