using System;

namespace MojBus.Data.Entities
{
    public partial class Gtfscalendar
    {
        public int ServiceId { get; set; }
        public int? Monday { get; set; }
        public int? Tuesday { get; set; }
        public int? Wednesday { get; set; }
        public int? Thursday { get; set; }
        public int? Friday { get; set; }
        public int? Saturday { get; set; }
        public int? Sunday { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AgencyId { get; set; }
        public int? ExternalServiceId { get; set; }
    }
}
