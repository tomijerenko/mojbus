using System;

namespace MojBus.Data.Entities
{
    public class TripPlannerEntity
    {
        public Int64 Id { get; set; }
        public string TripShortName { get; set; }
        public string TripHeadsign { get; set; }
        public int TripDirectionID { get; set; }
        public string HTMLColor { get; set; }
        public string RouteShortName { get; set; }
        public int StartStopId { get; set; }
        public int StartStopDirection { get; set; }
        public double StartStopLat { get; set; }
        public double StartStopLon { get; set; }
        public string StartStopName { get; set; }
        public int EndStopId { get; set; }
        public int EndStopDirection { get; set; }
        public double EndStopLat { get; set; }
        public double EndStopLon { get; set; }
        public string EndStopName { get; set; }
        public string StartStopDepartureTime { get; set; }
        public string EndStopArrivalTime { get; set; }
        public int TravelTimeMinutes { get; set; }
    }
}
