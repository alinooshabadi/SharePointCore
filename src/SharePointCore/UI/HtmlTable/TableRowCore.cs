using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.UI.HtmlTable
{ 
    public class TableRowCore
    {
        public string CssClass { get; set; }
        public List<TableCellCore> Cells { get; set; }
    }
}
