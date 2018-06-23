using System;
using System.Collections.Generic;

namespace MojBus.Data.Entities
{
    public partial class GtfsstopTimes
    {
        public int StopTimeId { get; set; }
        public int? TripId { get; set; }
        public int? ArrivalTime { get; set; }
        public int? DepartureTime { get; set; }
        public int? StopId { get; set; }
        public int? StopSequence { get; set; }
        public int? PickupType { get; set; }
        public int? DropoffType { get; set; }
    }
}
