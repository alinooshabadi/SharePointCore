using Microsoft.SharePoint.Administration;
using SharePointCore.UI;
using System;
using System.Web.UI;
using System.Collections.Generic;

namespace SharePointCore.Logging
{
    public class LoggerCore : SPDiagnosticsServiceBase, ILogger
    {
        public static string DiagnosticAreaName = "EIT";
        private static LoggerCore _Current;
        public static LoggerCore Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new LoggerCore();
                }

                return _Current;
            }
        }

        public LoggerCore() : base("EIT Logging Service", SPFarm.Local)
        {

        }

        public enum Category
        {
            Unexpected,
            High,
            Medium,
            Information
        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            var areas = new List<SPDiagnosticsArea>{
                new SPDiagnosticsArea(DiagnosticAreaName, new List<SPDiagnosticsCategory>            {
                    new SPDiagnosticsCategory("Unexpected", TraceSeverity.Unexpected, EventSeverity.Error),
                    new SPDiagnosticsCategory("High", TraceSeverity.High, EventSeverity.Warning),
                    new SPDiagnosticsCategory("Medium", TraceSeverity.Medium, EventSeverity.Information),
                    new SPDiagnosticsCategory("Information", TraceSeverity.Verbose, EventSeverity.Information)
                })
            };
            return areas;
        }
        public void Error(Exception ex)
        {
            var category = Current.Areas[DiagnosticAreaName].Categories[Category.Unexpected.ToString()];
            Current.WriteTrace(0, category, category.TraceSeverity, ex.ToString());
        }

        public void Error(Exception ex, Page page)
        {
            StatusBar.RenderStatusBar(page, "خطا", ex.Message, false, StatusBar.StatusBarColor.Red);
            Error(ex);
        }

        public void Info(string message)
        {
            var category = Current.Areas[DiagnosticAreaName].Categories[Category.Information.ToString()];
            Current.WriteTrace(0, category, category.TraceSeverity, message);
        }

        public void Info(string message, Page page)
        {
            StatusBar.RenderStatusBar(page, "توجه", message, false, StatusBar.StatusBarColor.Blue);            
        }

        public void Warning(string message, Page page)
        {
            StatusBar.RenderStatusBar(page, "توجه", message, false, StatusBar.StatusBarColor.Yellow);
            Warning(page.Title + "-" + message);
        }

        public void Warning(string message)
        {
            var category = Current.Areas[DiagnosticAreaName].Categories[Category.Medium.ToString()];
            Current.WriteTrace(0, category, category.TraceSeverity, message);
        }


        public bool Equals(LoggerCore other)
        {
            throw new NotImplementedException();
        }
    }
}
