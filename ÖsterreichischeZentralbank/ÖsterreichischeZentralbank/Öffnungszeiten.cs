using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖsterreichischeZentralbank
{
    public class Öffnungszeiten
    {
        public bool IsOpen(DateTime date)
        {
            TimeSpan startTime;
            TimeSpan endTime  ;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                   startTime = new TimeSpan(10, 30, 00);
                   endTime = new TimeSpan(19, 00, 00);

                    if (date.TimeOfDay >= startTime &&
                        date.TimeOfDay < endTime)
                        return true;
                    else
                        return false;
                case DayOfWeek.Saturday:
                    startTime = new TimeSpan(10, 30, 00);
                    endTime = new TimeSpan(14, 00, 00);

                    if (date.TimeOfDay >= startTime &&
                        date.TimeOfDay < endTime)
                        return true;
                    else
                        return false;
                case DayOfWeek.Sunday:
                    return false;
                default:
                    throw new Exception("Hier sollte man eig ned reinkommen");
            }

        }
    }
}
