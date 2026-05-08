using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Common.Exceptions
{
    public class AppValidationException: Exception
    {
        public Dictionary<string, string[]> Errors { get; }
        public AppValidationException(Dictionary<string, string[]> Errors)
        {
            this.Errors = Errors;
        }
    }
}
