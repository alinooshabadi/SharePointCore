using DNTPersianUtils.Core;
using System;

namespace SharePointCore.Extensions

{
    public static class StringExtensions
    {      
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }

        public static string ToPersianNumber(this string text)
        {
            return text.ToPersianNumbers();
        }

        public static string ToPersianDateString(this string text)
        {
            try
            {
                var date = DateTime.Parse(text);
                return date.ToPersianDateString();
            }
            catch(Exception ex)
            {
                return "";
            }
           
        }

        public static string ToNumericFormat(this string number)
        {
            double oD = 0;
            int oI = 0;
            if (double.TryParse(number, out oD))
                return string.Format("{0:#,###0}", oD);
            else if (int.TryParse(number, out oI))
                return string.Format("{0:#,###0}", oI);
            else
                return number;
        }
    }
}