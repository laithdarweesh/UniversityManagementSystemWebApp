using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Response_DTO
{
    public class PhoneDTO
    {
        public int PhoneId { get; }
        public string PhoneNumber { get; }
        public int PersonId { get; }
        public PhoneDTO(int PhoneId,string PhoneNumber,int PersonId)
        {
            this.PhoneId = PhoneId;
            this.PhoneNumber = PhoneNumber;
            this.PersonId = PersonId;
        }
    }
}