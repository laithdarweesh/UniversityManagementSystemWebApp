namespace UniversityManagementSystem.Application.Commands.Phones
{
    public class UpdatePhoneCommand
    {
        public string? PhoneNumber { get; }
        public UpdatePhoneCommand(string? phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}