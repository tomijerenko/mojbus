using System.Collections.Generic;

namespace MojBus.Models
{
    public class StopTimes
    {
        public int StopSequence { get; set; }
        public long StopId { get; set; }
        public string StopName { get; set; }
        public List<string> DepartureTimes { get; set; }
    }

    public class RouteStopsModel
    {
        public int TripID { get; set; }
        public int DirectionID { get; set; }
        public List<StopTimeData> Stops { get; set; }
    }

    public class StopTimeData
    {
        public string StopName { get; set; }
        public string DepartureTime { get; set; }
        public int StopSequence { get; set; }
    }
}
