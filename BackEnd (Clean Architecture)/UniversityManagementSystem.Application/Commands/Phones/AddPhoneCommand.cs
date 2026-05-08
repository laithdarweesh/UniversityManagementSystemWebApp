using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.Phones
{
    public class AddPhoneCommand
    {
        public string PhoneNumber { get; }
        public int PersonId { get; }
        public AddPhoneCommand(string PhoneNumber, int PersonId)
        {
            this.PhoneNumber = PhoneNumber;
            this.PersonId = PersonId;
        }
    }
}