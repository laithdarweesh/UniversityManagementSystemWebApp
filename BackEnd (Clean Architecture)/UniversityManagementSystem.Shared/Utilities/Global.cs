using System.Diagnostics;
using System.Runtime.Versioning;

namespace UniversityManagementSystem.Shared.Utilities
{
    [SupportedOSPlatform("windows")]
    public class Global
    {
        public static void SaveToEventLog(string Message, string SourceName, EventLogEntryType LogType)
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            // Log an Event
            EventLog.WriteEntry(SourceName, Message, LogType);
        }
        public static object ToDbNull<T>(T? value)
        {
            if (value == null)
                return DBNull.Value;

            if (value is string text && string.IsNullOrWhiteSpace(text))
                return DBNull.Value;

            return value;
        }
    }
}