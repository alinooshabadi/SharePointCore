namespace SharePointCore.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToStringSafe(this object obj)
        {
            if (obj == null)
                return string.Empty;

            return obj.ToString();
        }

        public static double ToDouble(this object input)
        {
            double output = 0;
            double.TryParse(input.ToStringSafe(), out output);
            return output;
        }

        public static int ToInt(this object input)
        {
            var output = 0;
            int.TryParse(input.ToStringSafe(), out output);
            return output;
        }
    }
}