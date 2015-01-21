namespace PhoneApp1.Helper
{
    public class StringHelper
    {
        public static string FormatDouble(string s)
        {
            return ParseDouble(s).ToString("N2");
        }

        public static string FormatInt(string s)
        {
            return ((int)ParseDouble(s)).ToString("N0");
        }

        public static double ParseDouble(string s)
        {
            double d;
            if (double.TryParse(s, out d) == true)
                return d;
            return -1;
        }

        public static int ParseInt(string s)
        {
            int d;
            if (int.TryParse(s, out d) == true)
                return d;
            return -1;
        }
    }
}
