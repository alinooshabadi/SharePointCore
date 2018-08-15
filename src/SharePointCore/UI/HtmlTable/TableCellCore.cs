using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharePointCore.UI.HtmlTable
{
    public class TableCellCore
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public Control Control { get; set; }

        public TableCellCore()
        {

        }

        public TableCellCore(string title)
        {
            this.Text = title;
        }
    }
}
