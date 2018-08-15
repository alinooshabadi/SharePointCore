using SharePointCore.UI.HtmlTable;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SharePointCore.Extensions.General
{
    public static class ControlExtensions
    {
        public static void GenerateTableCore(this Table table, IEnumerable<TableRowCore> rows)
        {
            var coreTable = TableCore.GenerateTable(rows);
            table.Rows.AddRange(coreTable.Rows.Cast<TableRow>().ToArray());
        }
    }
}
