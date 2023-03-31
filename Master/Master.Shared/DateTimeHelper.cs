using System;
using NodaTime;

namespace Master.Shared
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert UTC date time into proper time according to app time zone (IANA Tzdb ==> 'Europe/Berlin').
        /// </summary>
        public static DateTime UtcToZone(DateTime utcDateTime, string appTimeZone)
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(appTimeZone);

            if (timeZone == null)
                return utcDateTime;

            var instant = Instant.FromDateTimeUtc(DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc));
            var zonedDateTime = instant.InZone(timeZone);

            return zonedDateTime.ToDateTimeUnspecified();
        }

        /// <summary>
        /// Convert local date time in specific zone (IANA Tzdb ==> 'Europe/Berlin') into UTC.
        /// </summary>
        public static DateTime ZoneToUtc(DateTime dateTime, string appTimeZone)
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(appTimeZone);

            if (timeZone == null)
                return dateTime;

            var asLocal = LocalDateTime.FromDateTime(dateTime);
            var asZoned = asLocal.InZoneLeniently(timeZone);
            var asZonedInUtc = asZoned.WithZone(DateTimeZone.Utc);

            return asZonedInUtc.ToDateTimeUtc();
        }

        /// <summary>
        /// Calculate age depending on date of birth and current date.
        /// Returns age as number of years.
        /// </summary>
        public static int CalculateAge(DateTime dateOfBirth, DateTime today)
        {
            if (dateOfBirth > today)
                return 0;

            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }

        public static int GetUtcHourOffset(DateTime date, string appTimeZone)
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(appTimeZone);

            if (timeZone == null)
                return 0;

            var instant = Instant.FromDateTimeUtc(DateTime.SpecifyKind(date, DateTimeKind.Utc));

            return timeZone.GetUtcOffset(instant).Seconds / 3600;
        }
    }
}
