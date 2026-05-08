using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.Addresses
{
    public class AddAddressCommand
    {
        public string AddressName { get; }
        public int PersonId { get; }
        public AddAddressCommand(string AddressName, int PersonId)
        {
            this.AddressName = AddressName;
            this.PersonId = PersonId;
        }
    }
}