using System;
using System.Globalization;

namespace OnTrial.Common
{
    public static class DateTimeHelper
    {
        public static string CurrentTimeStamp
        {
            get { return DateTime.Now.ToString("ddMMyyyyHHmmss", CultureInfo.CurrentCulture); }
        }

        public static string CurrentDate
        {
            get { return DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.CurrentCulture); }
        }

        public static DateTime GetDate(TimeSpan? pOffset = null) =>
            DateTime.Now.Add(pOffset ?? TimeSpan.Zero);
    }
}