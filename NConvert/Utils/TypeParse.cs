using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Utils
{
    public class TypeParse
    {
        public static string DateTime2TimeStamp(string s)
        {
            DateTime dtNow = DateTime.Parse(s);
            return DateTime2TimeStamp(dtNow).ToString();
        }

        public static int DateTime2TimeStamp(DateTime dtNow)
        {
            if (dtNow.Year < 1990)
            {
                return 0;
            }
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            return int.Parse(timeStamp.Substring(0, timeStamp.Length - 7));
        }
    }
}
