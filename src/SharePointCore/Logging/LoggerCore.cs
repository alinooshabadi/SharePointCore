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
        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, Page page)
        {
            page.Response.Write(ex.Message);
        }
    }
}
