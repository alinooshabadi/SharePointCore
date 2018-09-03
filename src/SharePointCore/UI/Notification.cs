using Microsoft.SharePoint.WebControls;
using System;
using System.Web.UI;

namespace SharePointCore.UI
{
    public class Notification : UserControl
    {
        /// <summary>
        /// Gets or sets the text of the notification.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Adds the necessarry javascript to the page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RenderNotification(Page, Text);
        }

        /// <summary>
        /// Adds the necessarry javascript to a page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Text"></param>
        public static void RenderNotification(Page page, string Text)
        {
            RenderNotification(page, Text, false);
        }
        /// <summary>
        /// Adds the necessarry javascript to a page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Text"></param>
        /// <param name="sticky">Specifies whether the notification stays on the page until removed.</param>
        public static void RenderNotification(Page page, string Text, bool sticky)
        {
            // Add SP.js
            ScriptLink.RegisterScriptAfterUI(page, "SP.js", false, true);
            ScriptLink.RegisterScriptAfterUI(page, "SP.Ribbon.js", false, true);

            var script = @"                
                SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () { });
                ExecuteOrDelayUntilScriptLoaded(showNotification, 'SP.js');
                function showNotification(){{
                    " + GetNotificationScript(page, Text, sticky) + @"
                }}
            ";
            ScriptManager.RegisterClientScriptBlock(page, typeof(Notification), string.Format("Notification:{0}", Text), script, true);
        }
        /// <summary>
        /// retuns the script code to render a notification
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string GetNotificationScript(Page page, string Text)
        {
            return GetNotificationScript(page, Text, false);
        }
        /// <summary>
        /// retuns the script code to render a notification
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Text"></param>
        /// <param name="sticky">Specifies whether the notification stays on the page until removed.</param>
        /// <returns></returns>
        public static string GetNotificationScript(Page page, string Text, bool sticky)
        {
            return string.Format(@"SP.UI.Notify.addNotification(""{0}"", " + sticky.ToString().ToLower() + @");", Text);
        }
    }
}
