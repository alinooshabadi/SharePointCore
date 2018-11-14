using Microsoft.SharePoint;
using SharePointCore.Logging;
using System;
using System.Globalization;
using System.Threading;

namespace SharePointCore.Extensions.SharePoint
{
    public static class SPListExtensions
    {
        public static SPField TryGetField(this SPList list, string fieldName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            SPField field = null;
            try
            {
                if (list.Fields.ContainsField(fieldName))
                {
                    field = list.Fields[fieldName];
                }
                else
                {
                    field = list.Fields.GetFieldByInternalName(fieldName);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return field;
        }
    }
}
