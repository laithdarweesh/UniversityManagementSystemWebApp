namespace UniversityManagementSystem.Application.Commands.Addresses
{
    public class UpdateAddressCommand
    {
        public string? AddressName { get; }
        public UpdateAddressCommand(string? addressName)
        {
            this.AddressName = addressName;
        }
    }
}