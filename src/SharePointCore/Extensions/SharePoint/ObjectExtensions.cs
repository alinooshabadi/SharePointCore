namespace SharePointCore.Extensions
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

        public static double ToDouble(this object input)
        {
            try
            {
                return double.Parse(input.ToStringSafe());
            }
            catch
            {
                return 0;
            }
        }

        public static int ToInt(this object input)
        {
            try
            {
                return int.Parse(input.ToStringSafe());
            }
            catch
            {
                return 0;
            }
        }
    }
}