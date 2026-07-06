using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Addresses
{
    public class UpdateAddressRequest
    {
        [StringLength(500)]
        public string? AddressName { get; set; }
    }
}