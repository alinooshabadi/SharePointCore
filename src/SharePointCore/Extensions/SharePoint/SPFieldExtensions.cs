using Microsoft.SharePoint;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharePointCore.Extensions
{
    public static class SPFieldExtensions
    {
        public static void RenameField(this SPField fieldName, string newFieldName)
        {
            try
            {
                CultureInfo originalUICulture = Thread.CurrentThread.CurrentUICulture;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (SPContext.Current == null)
                    {
                        Thread.CurrentThread.CurrentUICulture =
                            new CultureInfo((int)fieldName.ParentList.ParentWeb.Language);
                    }
                    fieldName.Title = newFieldName;
                    fieldName.PushChangesToLists = true;
                    fieldName.Update(true);
                    fieldName.ParentList.Update();

                    Thread.CurrentThread.CurrentUICulture = originalUICulture;
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
