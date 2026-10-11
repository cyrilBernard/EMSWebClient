using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Text;

namespace webclient.Modules
{
    public class Utils
    {
        public static string ConvertDateTimeToString(DateTime? dateTime)
        {
            return dateTime?.ToString("d") ?? "";
        }
        public static string ConvertCurrencyToString(decimal? price)
        {
            return price?.ToString("N2") ?? "0.00";
        }
        public static string RandomPassword()
        {
            return RandomString(10,true);
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
