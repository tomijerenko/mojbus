using System;
using System.Collections.Generic;

namespace MojBus.Models
{
    public class TripPlannerModel
    {
        public DateTime RequestedDate { get; set; }
        public StopModel StartStop { get; set; }
        public StopModel EndStop { get; set; }
        public List<Line> Lines { get; set; }
    }

    public class StopModel
    {
        public int StopId { get; set; }
        public int StopDirection { get; set; }
        public double StopLat { get; set; }
        public double StopLon { get; set; }
        public string StopName { get; set; }
    }

    public class Line
    {
        public string TripShortName { get; set; }
        public string HTMLColor { get; set; }
        public string RouteShortName { get; set; }
        public List<Trip> Trips { get; set; }
    }


    public class Trip
    {        
        public int TripDirectionID { get; set; }
        public string TripHeadsign { get; set; }
        public List<TransitTime> Times { get; set; }
    }

    public class TransitTime
    {
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int TravelTimeMinutes { get; set; }
    }
}
