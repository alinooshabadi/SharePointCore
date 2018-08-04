using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharePointCore.Logging
{
    public class LoggerCore : ILogger
    {
        public Page CurrentPage { get; set; }
        public LoggerCore(Page page)
        {
            CurrentPage = page;
        }

        public void LogToOperations()
        {
            throw new NotImplementedException();
        }

        public void LogToOperations(string Message)
        {
            throw new NotImplementedException();
        }

        public void LogToOperations(Exception ex)
        {
            CurrentPage.Response.Write(ex.Message);
        }

        public void TraceToDeveloper()
        {
            throw new NotImplementedException();
        }

        public void TraceToDeveloper(string Message)
        {
            throw new NotImplementedException();
        }

        public void TraceToDeveloper(Exception ex)
        {
            CurrentPage.Response.Write(ex.Message);
        }
    }
}
