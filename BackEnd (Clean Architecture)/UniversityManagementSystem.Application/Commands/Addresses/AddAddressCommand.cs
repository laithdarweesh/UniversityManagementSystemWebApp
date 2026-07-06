namespace UniversityManagementSystem.Application.Commands.Addresses
{
    public class AddAddressCommand
    {
        public string AddressName { get; }
        public int PersonId { get; }
        public AddAddressCommand(string addressName, int personId)
        {
            this.AddressName = addressName;
            this.PersonId = personId;
        }
    }
}