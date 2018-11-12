using Microsoft.SharePoint.Utilities;
using System;

namespace SharePointCore.Logging
{
    public static class Monitoring
    {
        public static IDisposable StartHighScope(string name)
        {
            return new SPMonitoredScope("EIT Monitoring - " + name, Microsoft.SharePoint.Administration.TraceSeverity.High, new SPSqlQueryCounter());
        }

        public static IDisposable StartScope(string name, params ISPScopedPerformanceMonitor[] monitors)
        {
            return new SPMonitoredScope("EIT Monitoring - " + name, Microsoft.SharePoint.Administration.TraceSeverity.Verbose, monitors);
        }

        public static IDisposable StartScopeWithTimeLimit(string name, uint milliseconds, params ISPScopedPerformanceMonitor[] monitors)
        {
            return new SPMonitoredScope("EIT Monitoring - " + name, milliseconds, monitors);
        }
    }
}