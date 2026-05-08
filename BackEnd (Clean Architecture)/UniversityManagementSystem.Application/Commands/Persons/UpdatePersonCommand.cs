using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.Persons
{
    public class UpdatePersonCommand
    {
        public string? Email { get; }
        public string? ImagePath { get; }
        public UpdatePersonCommand(string? Email, string? ImagePath)
        {
            this.Email = Email;
            this.ImagePath = ImagePath;
        }
    }
}
