using System.Collections.Generic;
using System.Web.UI;

namespace SharePointCore.UI.HtmlTable
{
    public class TableCellCore
    {
        public string Text { get; set; }        
        public string CssClass { get; set; }
        public List<Control> Controls { get; set; }
       
    }
}
