using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UniversityManagementSystem.Shared.Utilities
{
    public class clsGlobal
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
        public static object ToDbNull<T>(T value)
        {
            if (value == null || (value is string strValue && string.IsNullOrWhiteSpace(strValue)))
                return DBNull.Value;


            if (Nullable.GetUnderlyingType(typeof(T)) != null && value.Equals(default(T)))
                return DBNull.Value;

            return value;
        }
    }
}
