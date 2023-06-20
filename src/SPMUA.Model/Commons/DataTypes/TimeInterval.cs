using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Commons.DataTypes
{
    public struct TimeInterval
    {
        public TimeInterval()
        {

        }
        public TimeInterval(TimeOnly startingTime, TimeOnly endingTime)
        {
            StartingTime = startingTime;
            EndingTime = endingTime;
        }

        public TimeOnly StartingTime { get; set; }
        public TimeOnly EndingTime { get; set; }

        public override string ToString()
        {
            return string.Concat(StartingTime.ToString("HH:mm"), " - ", EndingTime.ToString("HH:mm"));
        }
    }
}
