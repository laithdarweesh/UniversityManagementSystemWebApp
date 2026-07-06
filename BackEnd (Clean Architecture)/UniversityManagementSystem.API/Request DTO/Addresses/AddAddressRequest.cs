using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.API.Request_DTO.Addresses
{
    public class AddAddressRequest
    {
        [StringLength(500)]
        public required string AddressName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PersonId { get; set; }
    }
}