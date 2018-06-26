using System;
using System.Collections.Generic;

namespace MojBus.Models
{
    public class StopDataModel
    {
        public string TripShortName { get; set; }
        public string TripHeadsign { get; set; }
        public int DirectionID { get; set; }
        public int RouteId { get; set; }
        public List<string> DepartureTimes { get; set; }
    }
}
