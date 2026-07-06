using System.Diagnostics;
using System.Runtime.Versioning;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Infrastructure.Data
{
    [SupportedOSPlatform("windows")]
    public class EventLogLogger : IAppLog
    {
        private readonly string _sourceName;
        public EventLogLogger()
        {
            _sourceName = SettingsData.EventLogSourceName;

            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, "Application");
            }
        }
        public void LogInfo(string message)
        {
            EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Information);
        }
        public void LogError(string message)
        {
            EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Error);
        }
    }
}