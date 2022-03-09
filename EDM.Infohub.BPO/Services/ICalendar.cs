using CalendarService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services
{
    public interface ICalendar
    {
        DateTime GetNextDay(CalendarRequestCoppClark date);
    }
}
