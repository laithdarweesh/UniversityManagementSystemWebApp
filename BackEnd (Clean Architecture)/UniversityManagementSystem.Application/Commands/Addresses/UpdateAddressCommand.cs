using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.Addresses
{
    public class UpdateAddressCommand
    {
        public string AddressName { get; }
        public UpdateAddressCommand(string AddressName)
        {
            this.AddressName = AddressName;
        }
    }
}