using System;
using System.Collections.Generic;

namespace MojBus.Data.Entities
{
    public partial class Gtfstrips
    {
        public int TripId { get; set; }
        public int? RouteId { get; set; }
        public int? ServiceId { get; set; }
        public string TripHeadsign { get; set; }
        public string TripShortName { get; set; }
        public int? DirectionId { get; set; }
        public int? WheelchairAccesible { get; set; }
        public int? BikesAllowed { get; set; }
        public int? AgencyId { get; set; }
        public string ExternalTripId { get; set; }
    }
}
