using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Extensions.SharePoint
{
    public static class ObjectExtensions
    {
        public static string ToStringSafe(this object inString)
        {
            try
            {
                return inString.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
