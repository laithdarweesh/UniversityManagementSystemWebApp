using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Response_DTO
{
    public class MainFeesDTO
    {
        public int MainFeesID { get; }
        public string Title { get; }
        public decimal Fees { get; }
        public MainFeesDTO(int MainFeesID, string Title, decimal Fees)
        {
            this.MainFeesID = MainFeesID;
            this.Title = Title;
            this.Fees = Fees;
        }
    }
}