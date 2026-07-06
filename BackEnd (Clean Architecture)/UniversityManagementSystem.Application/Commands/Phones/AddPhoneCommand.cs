namespace UniversityManagementSystem.Application.Commands.Phones
{
    public class AddPhoneCommand
    {
        public string PhoneNumber { get; }
        public int PersonId { get; }
        public AddPhoneCommand(string phoneNumber, int personId)
        {
            this.PhoneNumber = phoneNumber;
            this.PersonId = personId;
        }
    }
}