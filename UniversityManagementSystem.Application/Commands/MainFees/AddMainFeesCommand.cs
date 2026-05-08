using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.MainFees
{
    public class AddMainFeesCommand
    {
        public string Title { get; }
        public decimal Fees { get; }
        public AddMainFeesCommand(string Title, decimal Fees)
        {
            this.Title = Title;
            this.Fees = Fees;
        }
    }
}