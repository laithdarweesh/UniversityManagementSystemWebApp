using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Commands.MainFees
{
    public class UpdateMainFeesCommand
    {
        public string Title { get; }
        public decimal Fees { get; }
        public UpdateMainFeesCommand(string Title, decimal Fees)
        {
            this.Title = Title;
            this.Fees = Fees;
        }
    }
}