namespace MojBus.Models.FavouriteStops
{
    public class FavouriteStopRouteModel
    {
        public string UserId { get; set; }
        public string StopName { get; set; }
        public string RouteShortName { get; set; }
        public int DirectionId { get; set; }
    }
}
