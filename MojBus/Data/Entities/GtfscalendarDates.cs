using System;

namespace MojBus.Data.Entities
{
    public partial class GtfscalendarDates
    {
        public int CalendarDateId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? Date { get; set; }
        public int? ExceptionType { get; set; }
    }
}
