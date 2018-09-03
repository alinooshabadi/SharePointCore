using SharePointCore.UI;
using System;
using System.Web.UI;

namespace SharePointCore.Logging
{
    public class LoggerCore : ILogger
    {
        public void Error(Exception ex)
        {
            //TODO
        }

        public void Error(Exception ex, Page page)
        {            
            StatusBar.RenderStatusBar(page, "خطا", ex.Message, false, StatusBar.StatusBarColor.Red);           
        }

        public void Info(string message)
        {
            //TODO
        }

        public void Info(string message, Page page)
        {
            StatusBar.RenderStatusBar(page, "توجه", message, false, StatusBar.StatusBarColor.Blue);
        }

        public void Warning(string message, Page page)
        {
            StatusBar.RenderStatusBar(page, "توجه", message, false, StatusBar.StatusBarColor.Yellow);
        }

        public void Warning(string message)
        {
            //TODO
        }
    }
}
