using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.Rules
{
    public class DateSelector
    {
        private const string EastAustraliaStandardTime = TimeZoneID.EastAustraliaStandardTime;
        private DateTime todaysDate;
        private TimeZoneInfo timeZoneInfo;

        public DateSelector(DateTime todaysDate, TimeZoneInfo timeZoneInfo)
        {
            this.todaysDate = todaysDate;            
            this.timeZoneInfo = timeZoneInfo;
        }

        public DateSelector()
        {
            this.todaysDate = DateTime.Today;
            this.timeZoneInfo = TimeZoneInfo.Local;
        }

        public DateTime GetTodaysDateInAustraliaAtMidnight()
        {
            return GetTodaysDateInAustralia().Date;
        }

        public DateTime GetTodaysDateInAustralia()
        {
            int differenceInHoursBetweenAustraliaAndLocalTimeZone = GetDifferenceInHoursBetweenAustraliaAndLocalTimeZone();
            return todaysDate.AddHours(differenceInHoursBetweenAustraliaAndLocalTimeZone);
        }

        private int GetDifferenceInHoursBetweenAustraliaAndLocalTimeZone()
        {
            if(this.timeZoneInfo == null)
            {
                return 0;
            }

            TimeZoneInfo australianTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(EastAustraliaStandardTime);
            return australianTimeZoneInfo.BaseUtcOffset.Hours - this.timeZoneInfo.BaseUtcOffset.Hours;
        }
    }
}
