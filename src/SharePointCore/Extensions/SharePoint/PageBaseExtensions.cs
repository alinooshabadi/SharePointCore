using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharePointCore.Extensions
{
    public static class PageBaseExtensions
    {
        public static void ChangePageTitle(this Page page, string title)
        {
            // Get a reference to the appropriate Content Placeholder
            var contentPlaceHolder = (ContentPlaceHolder)
                                   page.Master.FindControl("PlaceHolderPageTitle");

            // Clear out anything that SharePoint might have put in it already
            contentPlaceHolder.Controls.Clear();

            // Put your content in
            var literalControl = new LiteralControl
            {
                Text = title
            };
            contentPlaceHolder.Controls.Add(literalControl);
        }
    }
}