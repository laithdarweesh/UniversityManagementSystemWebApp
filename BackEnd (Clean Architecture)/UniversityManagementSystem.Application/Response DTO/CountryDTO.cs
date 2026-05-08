using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Response_DTO
{
    public class CountryDTO
    {
        public int CountryId { get; }
        public string CountryName { get; }
        public CountryDTO(int CountryId, string CountryName)
        {
            this.CountryId = CountryId;
            this.CountryName = CountryName;
        }
    }
}