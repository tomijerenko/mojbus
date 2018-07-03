namespace MojBus.Data.Entities
{
    public partial class Gtfsstops
    {
        public int StopId { get; set; }
        public string StopCode { get; set; }
        public string StopName { get; set; }
        public string StopDesc { get; set; }
        public double? StopLat { get; set; }
        public double? StopLon { get; set; }
        public string StopUrl { get; set; }
        public int? LocatyonType { get; set; }
        public int? ParentStationId { get; set; }
        public int? WeelchairBoarding { get; set; }
        public int? AgencyId { get; set; }
        public string ExternalStopId { get; set; }
    }
}
