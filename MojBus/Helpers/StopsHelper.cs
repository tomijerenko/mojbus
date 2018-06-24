using MojBus.Data;
using System;
using System.Linq;

namespace MojBus.Helpers
{
    public class StopsHelper
    {
        public static object GetStops(MojBusContext context)
        {
            return context.Gtfsstops.ToList();
        }

        public static object GetStops(MojBusContext context, int stopId)
        {
            DateTime todaysDate = DateTime.Today;

            var queryData = from stops in context.Gtfsstops
                            join stopTimes in context.GtfsstopTimes on stops.StopId equals stopTimes.StopId
                            join trips in context.Gtfstrips on stopTimes.TripId equals trips.TripId
                            join calendar in context.Gtfscalendar on trips.ServiceId equals calendar.ServiceId
                            join calendarDates in context.GtfscalendarDates on calendar.ServiceId equals calendarDates.ServiceId
                            join routes in context.Gtfsroutes on trips.RouteId equals routes.RouteId
                            where
                                 (calendar.StartDate < todaysDate && todaysDate < calendar.EndDate) &&
                                 calendarDates.Date != todaysDate &&
                                 stops.StopId == stopId
                            select new
                            {
                                routes,
                                trips,
                                stopTimes
                            };

            return queryData;
        }
    }
}
