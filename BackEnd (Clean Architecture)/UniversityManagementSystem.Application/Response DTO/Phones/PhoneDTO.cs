namespace UniversityManagementSystem.Application.Response_DTO.Phones
{
    public class PhoneDTO
    {
        public int PhoneId { get; }
        public string PhoneNumber { get; }
        public int PersonId { get; }
        public PhoneDTO(int phoneId, string phoneNumber, int personId)
        {
            this.PhoneId = phoneId;
            this.PhoneNumber = phoneNumber;
            this.PersonId = personId;
        }
    }
}