using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharePointCore.UI
{
    public class TableCore
    {
        public bool HasIndexCell { get; set; }
        public List<TableRowCore> Rows { get; set; }

        public TableCore()
        {

        }

        public Table GenerateTable()
        {
            return new Table();
        }
    }
    public class TableRowCore
    {
        public List<TableCellCore> Cells { get; set; }
    }

    public class TableCellCore
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public Control Control { get; set; }
    }
}
