using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.Phones
{
    public class UpdatePhoneCommand
    {
        public string PhoneNumber { get; set; }
        public UpdatePhoneCommand(string PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;
        }
    }
}