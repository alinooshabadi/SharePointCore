using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharePointCore.UI
{
    public class StatusBar : UserControl
    {
        /// <summary>
        /// Gets or sets the title of the message (bold).
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the text of the message.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets, whether the statusbar should render invisible.
        /// </summary>
        public bool HideAtBeginning { get; set; }
        /// <summary>
        /// Gets or sets the color of the statusbar.
        /// </summary>
        public StatusBarColor Color { get; set; }

        /// <summary>
        /// Renders the necessarry javascript on the page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RenderStatusBar(Page, Title, Text, HideAtBeginning, Color);
        }

        /// <summary>
        /// Available colors for the status bar.
        /// </summary>
        public enum StatusBarColor
        {
            /// <summary>
            /// No color is set.
            /// </summary>
            None,
            /// <summary>
            /// Red background color
            /// </summary>
            Red,
            /// <summary>
            /// Blue background color
            /// </summary>
            Blue,
            /// <summary>
            /// Green background color
            /// </summary>
            Green,
            /// <summary>
            /// Yellow bachground color
            /// </summary>
            Yellow
        }

        /// <summary>
        /// Renders the necessarry javascript on a given page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Title"></param>
        /// <param name="Text"></param>
        /// <param name="HideAtBeginning"></param>
        /// <param name="Color"></param>
        public static void RenderStatusBar(Page page, string Title, string Text, bool HideAtBeginning, StatusBarColor Color)
        {
            ScriptLink.RegisterScriptAfterUI(page, "SP.js", false, true);
            var script = string.Format(@"
               window.onload = function () {{                
                ExecuteOrDelayUntilScriptLoaded(addStatusBar, 'SP.js');
                function addStatusBar(){{
                    var sid = SP.UI.Status.addStatus(""{0}"", ""{1}"", {2});
                    {3}
                }}
                }}
            ", Title, Text, (!HideAtBeginning).ToString().ToLower(),
            Color != StatusBarColor.None ? string.Format("SP.UI.Status.setStatusPriColor(sid, '{0}');", Enum.GetName(typeof(StatusBarColor), Color).ToLower()) : "");            
            ScriptManager.RegisterClientScriptBlock(page, typeof(StatusBar), string.Format("StatusBar:{0}", Title), script, true);
        }
    }
}
