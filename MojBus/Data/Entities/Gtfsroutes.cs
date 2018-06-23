using System;
using System.Collections.Generic;

namespace MojBus.Data.Entities
{
    public partial class Gtfsroutes
    {
        public int RouteId { get; set; }
        public int? AgencyId { get; set; }
        public string RouteShortName { get; set; }
        public string RouteLongName { get; set; }
        public string RouteDesc { get; set; }
        public int? RouteType { get; set; }
        public string RouteUrl { get; set; }
        public string RouteColor { get; set; }
        public string RouteTextColor { get; set; }
        public int? RouteSortOrder { get; set; }
        public string ExternalRouteId { get; set; }
    }
}
