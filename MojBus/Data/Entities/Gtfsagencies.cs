using System;
using System.Collections.Generic;

namespace MojBus.Data.Entities
{
    public partial class Gtfsagencies
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyUrl { get; set; }
        public string AgencyTimeZone { get; set; }
        public string AgencyPhone { get; set; }
        public string AgencyLang { get; set; }
        public string AgencyEmail { get; set; }
        public string AgencyFareUrl { get; set; }
        public string ExternalAgencyId { get; set; }
    }
}
