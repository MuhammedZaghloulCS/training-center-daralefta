using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public static class TimeExtensions
    {
        public static string ToArabic12Hour(this TimeSpan time)
        {
            return DateTime.Today.Add(time)
                .ToString("hh:mm tt")
                .Replace("AM", "صباحاً")
                .Replace("PM", "مساءً");
        }
    }
}
