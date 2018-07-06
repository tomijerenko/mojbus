namespace MojBus.Data.Entities
{
    public partial class FavouriteStopRoutes
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string StopName { get; set; }
        public string RouteShortName { get; set; }
        public int DirectionId { get; set; }
    }
}
