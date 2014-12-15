using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkDays(this DateTime date, int workingDays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);
                if (newDate.DayOfWeek != DayOfWeek.Saturday &&
                    newDate.DayOfWeek != DayOfWeek.Sunday &&
                    !newDate.IsHoliday())
                {
                    workingDays -= direction;
                }
            }
            return newDate;
        }

        /// <summary>
        /// Informe si c'est un jour ouvert (en France)
        /// </summary>
        /// <param name="date"> date </param>
        /// <returns></returns>
        public static bool IsWorkingDay(this DateTime date)
        {
            if (DayOfWeek.Saturday.Equals(date.DayOfWeek) ||
                DayOfWeek.Sunday.Equals(date.DayOfWeek) ||
                date.IsHoliday())
            {
                return false;
            }
            return true;
        }

        public static bool IsHoliday(this DateTime date)
        {
            List<DateTime> holidays = new List<DateTime>();
            
            holidays.Add(new DateTime(2014, 01, 01));

            return holidays.Contains(date.Date);
        }

        /// <summary>
        /// Donne le nombre de jours ouvrés entre deux dates
        /// </summary>
        /// <param name="otherDate"> autre date </param>
        /// <param name="currentDate"> date actuelle </param>
        /// <returns> le nombre de jours entre date de début ou date de fin, 0 si ce ne sont pas des jours ouvrés</returns>
        public static int WorkingDaysFromBetweenDates(this DateTime currentDate, DateTime otherDate)
        {
            if (!IsWorkingDay(otherDate) || !IsWorkingDay(currentDate) || otherDate.CompareTo(currentDate) == 0)
            {
                return 0;
            }

            int nbDays = 0;

            DateTime d = otherDate;

            DateTime maxDate = currentDate;

            if (otherDate > currentDate)
            {
                maxDate = otherDate;
                d = currentDate;
            }

            while (d <= maxDate)
            {
                d = d.AddWorkDays(1);

                nbDays++;
            }
            
            return nbDays;
        }
    }
}
