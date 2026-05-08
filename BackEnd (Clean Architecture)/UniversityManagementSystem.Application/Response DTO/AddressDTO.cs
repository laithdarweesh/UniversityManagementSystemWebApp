using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Response_DTO
{
    public class AddressDTO
    {
        public int AddressID { get; }
        public string AddressName { get; }
        public int PersonId { get; }
        public AddressDTO(int AddressID, string AddressName, int PersonId)
        {
            this.AddressID = AddressID;
            this.AddressName = AddressName;
            this.PersonId = PersonId;
        }
    }
}