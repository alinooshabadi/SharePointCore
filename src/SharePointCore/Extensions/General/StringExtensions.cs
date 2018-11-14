namespace SharePointCore.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the specified System.String object is null or an System.String.Empty string
        /// </summary>
        /// <param name="instance">todo: describe instance parameter on IsNullOrEmpty</param>
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }

        public static string ToPersianNumbers(this string text)
        {
            return text.ToPersianNumbers();
        }
    }
}