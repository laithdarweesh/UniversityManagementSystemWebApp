using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAppLog
    {
        void LogInfo(string Message);
        void LogError(string Message);
    }
}
