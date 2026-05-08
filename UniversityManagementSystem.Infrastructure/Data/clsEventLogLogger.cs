using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data
{
    public class clsEventLogLogger: IAppLog
    {
        private readonly string _SourceName;
        public clsEventLogLogger()
        {
            _SourceName = clsSettingsData.EventLogSourceName;

            if(!EventLog.SourceExists(_SourceName))
            {
                EventLog.CreateEventSource(_SourceName, "Application");
            }
        }
        public void LogInfo(string Message)
        {
            EventLog.WriteEntry(_SourceName, Message, EventLogEntryType.Information);
        }
        public void LogError(string Message)
        {
            EventLog.WriteEntry(_SourceName, Message, EventLogEntryType.Error);
        }
    }
}
