using System;
using System.Collections.Generic;
using System.Linq;

namespace AndiSoft.Utilities.Extensions
{
    /// <summary>
    /// DateTime extension
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Takes a date, and adds the given workdays
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workDays">Amount of workdays to add</param>
        /// <param name="holidays">If present, these dates will be ignored as workdays</param>
        /// <returns></returns>
        public static DateTime AddWorkdays(this DateTime date, int workDays, List<DateTime> holidays = null)
        {
            var tmpDate = date;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(1);

                if (tmpDate.IsWorkDay() && !tmpDate.IsHoliday(holidays))
                    workDays--;
            }
            return tmpDate;
        }

        /// <summary>
        /// Check if a date is work day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWorkDay(this DateTime date)
        {
            return (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
        }


        /// <summary>
        /// Check if a given date is holiday according to the given holiday list
        /// </summary>
        /// <param name="date"></param>
        /// <param name="holidays">Holiday list</param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime date, List<DateTime> holidays)
        {
            if (holidays == null || !holidays.Any())
                return false;

            return holidays.Contains(date);
        }
    }
}
