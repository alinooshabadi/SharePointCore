using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Logging
{
    public interface ILogger
    {
        void LogToOperations();
        void LogToOperations(Exception ex);
        void LogToOperations(string Message);
        void TraceToDeveloper();
        void TraceToDeveloper(Exception ex);
        void TraceToDeveloper(string Message);
    }
}
