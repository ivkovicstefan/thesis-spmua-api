using SPMUA.Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static TimeInterval CreateAppointmentTimeInterval(TimeOnly time, int duration, bool isRoundToNextHourEnabled = true)
        {
            return new TimeInterval(time, isRoundToNextHourEnabled ? RoundToNextHour(time.AddMinutes(duration)) 
                                                                   : time.AddMinutes(duration));
        }

        public static string ReplaceTemplateWithParamData<T> (string template, T paramData) 
        {
            Type dataType = typeof (T);

            PropertyInfo[] properties = dataType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(paramData)?.ToString();

                string placeholder = $"{{{{{propertyName}}}}}";
                template = template.Replace(placeholder, propertyValue);
            }

            return template;
        }
    }
}
