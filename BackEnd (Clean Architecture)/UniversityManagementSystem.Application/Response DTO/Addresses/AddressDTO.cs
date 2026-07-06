namespace UniversityManagementSystem.Application.Response_DTO.Addresses
{
    public class AddressDTO
    {
        public int AddressId { get; }
        public string AddressName { get; }
        public int PersonId { get; }
        public AddressDTO(int addressId, string addressName, int personId)
        {
            this.AddressId = addressId;
            this.AddressName = addressName;
            this.PersonId = personId;
        }
    }
}