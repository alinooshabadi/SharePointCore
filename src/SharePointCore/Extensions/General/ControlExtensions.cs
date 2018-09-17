using SharePointCore.UI.HtmlTable;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SharePointCore.Extensions.General
{
    public static class ControlExtensions
    {
        public static void GenerateTableCore(this Table table, IEnumerable<TableRowCore> rows, bool hasIndexCell = false)
        {
            var coreTable = TableCore.GenerateTable(rows, hasIndexCell);
            table.Rows.AddRange(coreTable.Rows.Cast<TableRow>().ToArray());
        }

        public static TableCellCore ToCellCore(this object obj, string cssClass = "")
        {
            if(obj == null)
                return new TableCellCore { Text = "-" };

            if (obj is string)
                return new TableCellCore { Text = obj.ToString(), CssClass = cssClass };

            if (obj is int)
                return new TableCellCore { Text = obj.ToString(), CssClass = cssClass };

            if (obj is Control)
                return new TableCellCore { Controls = new List<Control> { obj as Control }, CssClass = cssClass };

            if (obj is List<Control>)
                return new TableCellCore { Controls = obj as List<Control>, CssClass = cssClass };

            if (obj is HtmlGenericControl)
                return new TableCellCore { Controls = new List<Control> { obj as HtmlGenericControl }, CssClass = cssClass };

            if (obj is List<HtmlGenericControl>)
            {
                var controls = new List<Control>();
                controls.AddRange((obj as List<HtmlGenericControl>).ToArray());
                return new TableCellCore { Controls = controls, CssClass = cssClass };
            }

            return new TableCellCore();
        }
    }
}
