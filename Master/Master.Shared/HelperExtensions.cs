using System;
using System.Collections;

namespace Lila.Platform.Shared
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Check if generic array is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Check if list is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this IList list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// Get the start date of week of certain date
        /// </summary>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
