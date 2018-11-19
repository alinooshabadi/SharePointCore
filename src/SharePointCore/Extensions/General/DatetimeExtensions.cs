using DNTPersianUtils.Core;
using System;

namespace SharePointCore.Extensions
{
    public static class DatetimeExtensions
    {
        public static string ToHumanTimeString(this TimeSpan timeSpan)
        {
            var elapsedTime = string.Empty;
            if (timeSpan.Days > 0)
            {
                elapsedTime = string.Format("{0:%d} روز و {0:%h} ساعت و {0:%m} دقیقه ", timeSpan);
            }
            else if (timeSpan.Hours > 0)
            {
                elapsedTime = string.Format("{0:%h} ساعت و {0:%m} دقیقه ", timeSpan);
            }
            else if (timeSpan.Minutes > 0)
            {
                elapsedTime = string.Format("{0:%m} دقیقه ", timeSpan);
            }
            else if (timeSpan.Seconds > 0)
            {
                elapsedTime = string.Format("{0:%s} ثانیه ", timeSpan);
            }

            return elapsedTime;
        }

        public static string ToHumanTimeStringInDays(this TimeSpan timeSpan)
        {
            var elapsedTime = string.Empty;
            if (timeSpan.Days > 0)
            {
                elapsedTime = string.Format("{0:%d} روز", timeSpan);
            }
            else
            {
                elapsedTime = "1 روز";
            }

            return elapsedTime;
        }

        public static string ToPersianDateString(this DateTime? date)
        {
            var pdate = string.Empty;
            if (date.HasValue)
            {
                pdate = date.Value.ToPersianDateTextify();
            }

            return pdate;
        }

        public static string ToPersianDateString(this DateTime date)
        {
            return date.ToPersianDateTextify();
        }

        public static string ToPersianDateString(this DateTime date, string format)
        {
            return date.ToPersianDateTimeString(format);
        }

        public static string ToPersianDateString(this DateTime? date, string format)
        {
            var pdate = string.Empty;
            if (date.HasValue)
            {
                return date.Value.ToPersianDateTimeString(format);
            }

            return pdate;
        }

        public static string ToRelativeTime(this DateTime date)
        {
            return date.ToFriendlyPersianDateTextify();
        }

        public static string GetAge(this DateTime date)
        {
            return DateTimeUtils.GetAge(date).ToString();
        }

        public static DateTime? ToGregorianDateTime(this string date)
        {
            return PersianDateTimeUtils.ToGregorianDateTime(date);
        }
    }
}