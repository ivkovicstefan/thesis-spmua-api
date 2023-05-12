using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Utility.Helpers
{
    public static class Helper
    {
        public static TimeOnly RoundToNextHour(TimeOnly time)
        {
            int deltaTimeInMinutes = 0 - time.Minute;

            if (deltaTimeInMinutes != 0)
            {
                time = time.AddMinutes(deltaTimeInMinutes).AddHours(1);
            }

            return time;
        }
    }
}
