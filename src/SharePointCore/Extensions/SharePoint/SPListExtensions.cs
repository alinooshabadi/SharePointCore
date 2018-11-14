using Microsoft.SharePoint;
using SharePointCore.Logging;
using System;
using System.Globalization;
using System.Threading;

namespace SharePointCore.Extensions
{
    public static class SPListExtensions
    {
        public static SPField TryGetField(this SPList list, string fieldName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            SPField field = null;
            try
            {
                if (list.Fields.ContainsFieldWithStaticName(fieldName))
                {
                    field = list.Fields.GetFieldByInternalName(fieldName);
                }
                else
                {
                    field = list.Fields[fieldName];
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