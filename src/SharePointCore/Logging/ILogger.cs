using System;
using System.Web.UI;

namespace SharePointCore.Logging
{
    public interface ILogger
    {        
        void Error(Exception ex, Page page);
        void Error(Exception ex);

        void Warning(string message, Page page);
        void Warning(string message);

        void Info(string message, Page page);
        void Info(string message);

    }
}
