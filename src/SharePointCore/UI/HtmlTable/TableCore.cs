using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SharePointCore.UI.HtmlTable
{
    public static class TableCore
    {
        public static Table GenerateTable(IEnumerable<TableRowCore> Rows, bool HasIndexCell = false)
        {
            var table = new Table();
            foreach (var row in Rows)
            {
                var tRow = new TableRow();
                foreach (TableCellCore cell in row.Cells)
                {
                    var tCell = new TableCell
                    {
                        Text = cell.Text,
                        CssClass = cell.CssClass
                    };
                    tRow.Cells.Add(tCell);
                }
                table.Rows.Add(tRow);
            }
            return table;
        }
    }
}
