using Microsoft.SharePoint.WebControls;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.UI
{
    public class LayoutsPageBaseCore : LayoutsPageBase
    {
        public ILogger Logger { get; set; }
        protected override void OnInit(EventArgs e)
        {
            Logger = new LoggerCore(this.Page);
        }
    }
}
