using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Extensions
{
    public static class NumberExtensiosns
    {
        public static string ReadableTime(this double day)
        {
            var parts = new List<string>();
            Action<int, string> add = (val, unit) => { if (val > 0) { parts.Add(val + unit); } };
            var t = TimeSpan.FromDays(day);

            add(t.Days, " روز");
            //add(t.Hours, " ساعت");
            //add(t.Minutes, " دقیقه");
            //add(t.Seconds, "s");
            //add(t.Milliseconds, "ms");

            return string.Join(" و ", parts);
        }

        public static double Round(this double target, int decimals)
        {
            return Math.Round(target, decimals);
        }

        public static string ToPersianNumber(this double text)
        {
            return text.ToStringSafe().ToPersianNumbers();
        }
    }
}