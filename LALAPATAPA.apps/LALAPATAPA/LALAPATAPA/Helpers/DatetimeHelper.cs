using System;
using System.Collections.Generic;
using System.Text;

namespace LALAPATAPA.Helpers
{
    public static class DatetimeHelper
    {
        public static DateTime GetDatetimeNow()
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;

            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT+07:00");
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            return dateTime;
        }
    }
}
