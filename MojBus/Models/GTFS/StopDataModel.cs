using System;
using System.Collections.Generic;

namespace MojBus.Models
{
    public class StopTimetable
    {
        public int DirectionId { get; set; }
        public string StopName { get; set; }
        public string RouteShortName { get; set; }
        public DateTime RequestedDate { get; set; }
        public StopLocationModel StopLocation { get; set; }
        public List<StopDataModel> StopTimetables { get; set; }
    }

    public class StopLocationModel
    {
        public double? StopLon { get; set; }
        public double? StopLat { get; set; }
    }

    public class StopDataModel
    {
        public string HTMLColor { get; set; }
        public string StopName { get; set; }
        public double StopLon { get; set; }
        public double StopLat { get; set; }
        public string TripShortName { get; set; }
        public string TripHeadsign { get; set; }
        public int DirectionID { get; set; }
        public int RouteId { get; set; }
        public bool isFavourite { get; set; }
        public List<string> DepartureTimes { get; set; }
    }
}
