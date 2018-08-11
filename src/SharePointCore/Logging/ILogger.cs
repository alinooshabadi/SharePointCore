using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharePointCore.Logging
{
    public interface ILogger
    {        
        void Error(Exception ex, Page page);
        void Error(Exception ex);
        
    }
}
