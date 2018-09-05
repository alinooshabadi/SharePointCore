using Microsoft.SharePoint.Utilities;
using System;

namespace SharePointCore.Logging
{
    public static class Monitoring
    {
        public static IDisposable StartHighScope(string name)
        {
            return new SPMonitoredScope(name, Microsoft.SharePoint.Administration.TraceSeverity.High);
        }

        public static IDisposable StartScope(string name, params ISPScopedPerformanceMonitor[] monitors)
        {
            return new SPMonitoredScope(name, Microsoft.SharePoint.Administration.TraceSeverity.Verbose, monitors);
        }

        public static IDisposable StartScopeWithTimeLimit(string name, uint milliseconds, params ISPScopedPerformanceMonitor[] monitors)
        {
            return new SPMonitoredScope(name, milliseconds, monitors);
        }
    }
}