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
        
        public static bool IsHoliday(this DateTime date)
        {
            List<DateTime> holidays = new List<DateTime>();
            
            holidays.Add(new DateTime(2014, 01, 01));

            return holidays.Contains(date.Date);
        }
    }
}
