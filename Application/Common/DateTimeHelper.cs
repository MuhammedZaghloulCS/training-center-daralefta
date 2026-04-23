using System;

namespace Application.Common
{
    /// <summary>
    /// Helper class for Cairo timezone (UTC+2)
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo CairoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

        /// <summary>
        /// Gets the current Cairo local time
        /// </summary>
        public static DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CairoTimeZone);

        /// <summary>
        /// Converts a UTC DateTime to Cairo local time
        /// </summary>
        public static DateTime FromUtc(DateTime utcDateTime)
        {
            if (utcDateTime.Kind == DateTimeKind.Local)
                return utcDateTime;
            
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, CairoTimeZone);
        }

        /// <summary>
        /// Converts a Cairo local DateTime to UTC
        /// </summary>
        public static DateTime ToUtc(DateTime localDateTime)
        {
            if (localDateTime.Kind == DateTimeKind.Utc)
                return localDateTime;
            
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, CairoTimeZone);
        }
    }
}
