using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SharePointCore.UI.HtmlTable
{
    public static class TableCore
    {
        public static Table GenerateTable(IEnumerable<TableRowCore> Rows, bool HasIndexCell = false)
        {
            var table = new Table();
            foreach (var row in Rows.Select((value, i) => new { i, value }))
            {
                var tRow = new TableRow { CssClass = row.value.CssClass };
                if (HasIndexCell)
                {
                    var indexCell = new TableCell { Text = (row.i + 1).ToString() };
                    tRow.Cells.Add(indexCell);
                }

                foreach (TableCellCore cell in row.value.Cells)
                {
                    var tCell = new TableCell
                    {
                        Text = cell?.Text,
                        CssClass = cell?.CssClass
                    };
                    if (cell != null && cell.Controls != null && cell.Controls.Any())
                        cell.Controls.ForEach(x => tCell.Controls.Add(x));

                    tRow.Cells.Add(tCell);
                }
                table.Rows.Add(tRow);
            }
            return table;
        }
    }
}